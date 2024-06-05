using Cello.Application.Repositories;
using Cello.Domain.Entities;
using Cello.Infrastructure.Common.Context;
using Microsoft.EntityFrameworkCore;

namespace Cello.Infrastructure.Common.Repositories
{
    public class UserRepository : IUserRepository
    {
        protected readonly ICelloDbContext Context;
        public UserRepository(ICelloDbContext context)
        {
            Context = context;
        }

        public void Create(User entity)
        {
            Context.Add(entity);
        }

        public Task<User?> Get(Guid id, CancellationToken cancellationToken)
        {
            return Context.Users.Where(c => c.Id == id).FirstOrDefaultAsync(cancellationToken);
        }

        public Task<List<User>> GetAll(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<User?> GetByEmail(string email, CancellationToken cancellationToken)
        {
            return Context.Set<User>().FirstOrDefaultAsync(x => x.Email == email, cancellationToken);
        }

        public Task<(List<User>, long)> GetList(int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
