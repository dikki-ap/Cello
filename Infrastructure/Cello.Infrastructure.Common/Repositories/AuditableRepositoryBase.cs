using Cello.Application.Repositories;
using Cello.Domain.Common;
using Cello.Infrastructure.Common.Context;
using Microsoft.EntityFrameworkCore;

namespace Cello.Infrastructure.Common.Repositories
{
    public abstract class AuditableRepositoryBase<T> : IAuditableRepositoryBase<T> where T : AuditableEntityBase
    {
        protected readonly ICelloDbContext Context;

        public AuditableRepositoryBase(ICelloDbContext context)
        {
            Context = context;
        }

        /// <summary>
        /// Create new entity
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task CreateAsync(T entity, CancellationToken cancellationToken)
        {
            // Set CreatedAt to UtcNow
            entity.CreatedAt = DateTime.UtcNow;

            // Set CreatedBy based on CreatedById
            entity.CreatedBy = Context.Users.FirstOrDefault(c => c.Id == entity.CreatedById);
            await Context.AddAsync(entity, cancellationToken);
        }

        public async Task BulkCreateAsync(T[] entities, CancellationToken cancellationToken)
        {
            var now = DateTime.UtcNow;
            foreach (var entity in entities)
            {
                entity.CreatedAt = now;
            }
            await Context.AddRangeAsync(entities, cancellationToken);
        }

        /// <summary>
        /// Update current entity value
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="cancellationToken"></param>
        public void Update(T entity, CancellationToken cancellationToken)
        {
            // Set UpdatedAt to UtcNow
            entity.UpdatedAt = DateTime.UtcNow;

            // Set UpdatedBy based on UpdatedById
            entity.UpdatedBy = Context.Users.FirstOrDefault(c => c.Id == entity.UpdatedById);
            Context.Update(entity);
        }

        /// <summary>
        /// Update current entity value
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="cancellationToken"></param>
        public void Delete(T entity, CancellationToken cancellationToken)
        {
            // Set DeletedAt to UtcNow
            entity.DeletedAt = DateTime.UtcNow;

            // Set DeletedBy based on DeletedById
            entity.DeletedBy = Context.Users.FirstOrDefault(c => c.Id == entity.DeletedById);
            Context.Update(entity);
        }

        /// <summary>
        /// Update current entity value
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="cancellationToken"></param>
        public void Undelete(T entity, CancellationToken cancellationToken)
        {
            // Set UpdatedBy to current user and DeletedBy to null
            entity.CreatedBy = Context.Users.FirstOrDefault(c => c.Id == entity.CreatedById);
            entity.UpdatedAt = DateTime.UtcNow;
            entity.UpdatedBy = Context.Users.FirstOrDefault(c => c.Id == entity.UpdatedById);
            entity.DeletedAt = null;
            entity.DeletedById = null;
            entity.DeletedBy = null;
            Context.Update(entity);
        }

        /// <summary>
        /// Get specific entity value using Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<T?> GetAsync(int id, CancellationToken cancellationToken)
        {
            return Context.Set<T>()
                .Include(x => x.CreatedBy)
                .Include(x => x.UpdatedBy)
                .Include(x => x.DeletedBy)
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        /// <summary>
        /// Get all entities
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<List<T>> GetAllAsync(CancellationToken cancellationToken)
        {
            return Context.Set<T>()
                .Include(x => x.CreatedBy)
                .Include(x => x.UpdatedBy)
                .Include(x => x.DeletedBy)
                .ToListAsync(cancellationToken);
        }
    }
}
