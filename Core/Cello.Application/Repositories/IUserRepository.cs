using Cello.Domain.Entities;

namespace Cello.Application.Repositories
{
    public interface IUserRepository
    {
        void Create(User entity);
        Task<User?> Get(Guid id, CancellationToken cancellationToken);
        Task<User?> GetByEmail(string email, CancellationToken cancellationToken);
        Task<List<User>> GetAll(CancellationToken cancellationToken);
        Task<(List<User>, long)> GetList(int pageNumber, int pageSize, CancellationToken cancellationToken);
    }
}
