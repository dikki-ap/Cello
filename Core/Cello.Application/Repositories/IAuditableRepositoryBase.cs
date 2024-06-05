namespace Cello.Application.Repositories
{
    public interface IAuditableRepositoryBase<T> where T : Domain.Common.AuditableEntityBase
    {
        Task CreateAsync(T entity, CancellationToken cancellationToken);
        void Update(T entity, CancellationToken cancellationToken);
        void Delete(T entity, CancellationToken cancellationToken);
        void Undelete(T entity, CancellationToken cancellationToken);
        Task<T?> GetAsync(int id, CancellationToken cancellationToken);
        Task<List<T>> GetAllAsync(CancellationToken cancellationToken);
    }
}
