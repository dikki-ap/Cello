using Cello.Application.Repositories;
using Cello.Infrastructure.Common.Context;

namespace Cello.Infrastructure.Common.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ICelloDbContext _context;
        private IDbTransaction? _currentTransaction;

        public UnitOfWork(ICelloDbContext context)
        {
            _context = context;
        }

        public async Task BeginTransactionAsync(CancellationToken cancellationToken)
        {
            if (_currentTransaction is not null)
            {
                throw new InvalidOperationException("A transaction has already been started.");
            }
            _currentTransaction = await _context.BeginTransactionAsync(cancellationToken);
        }

        public async Task CommitAsync(CancellationToken cancellationToken)
        {
            if (_currentTransaction is null)
            {
                throw new InvalidOperationException("A transaction has not been started.");
            }
            try
            {
                await _currentTransaction.CommitAsync();
                _currentTransaction.Dispose();
                _currentTransaction = null;
            }
            catch (Exception)
            {
                if (_currentTransaction is not null)
                    await _currentTransaction.RollbackAsync();
                throw;
            }
        }

        public Task SaveAsync(CancellationToken cancellationToken)
        {
            return _context.SaveChangesAsync(cancellationToken);
        }
    }
}
