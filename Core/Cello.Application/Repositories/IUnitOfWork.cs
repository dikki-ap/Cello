namespace Cello.Application.Repositories
{
    public interface IUnitOfWork
    {
        Task SaveAsync(CancellationToken cancellationToken);
        Task BeginTransactionAsync(CancellationToken cancellationToken);
        Task CommitAsync(CancellationToken cancellationToken);
    }
}
