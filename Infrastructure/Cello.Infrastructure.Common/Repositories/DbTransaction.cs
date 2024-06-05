using Microsoft.EntityFrameworkCore.Storage;

namespace Cello.Infrastructure.Common.Repositories
{
    public class DbTransaction(IDbContextTransaction dbContextTransaction) : Cello.Application.Repositories.IDbTransaction
    {
        private readonly IDbContextTransaction _dbContextTransaction = dbContextTransaction;

        public Task CommitAsync()
        {
            try
            {
                return _dbContextTransaction.CommitAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Dispose()
        {
            try
            {
                _dbContextTransaction.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Task RollbackAsync()
        {
            try
            {
                return _dbContextTransaction.RollbackAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
