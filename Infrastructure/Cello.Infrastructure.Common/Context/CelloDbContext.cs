using Cello.Application.Repositories;
using Cello.Domain.Entities;
using Cello.Infrastructure.Common.Comparers;
using Cello.Infrastructure.Common.Repositories;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Reflection;

namespace Cello.Infrastructure.Common.Context
{
    public class CelloDbContext : DbContext, ICelloDbContext
    {
        public CelloDbContext(DbContextOptions<CelloDbContext> options) : base(options) { }

        public virtual DbSet<User> Users => Set<User>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
        // omitted for brevity
        protected override void ConfigureConventions(ModelConfigurationBuilder builder)
        {
            base.ConfigureConventions(builder);
            builder.Properties<DateOnly>()
                .HaveConversion<DateOnlyConverter, DateOnlyComparer>()
                .HaveColumnType("date");
            builder.Properties<TimeOnly>()
                .HaveConversion<TimeOnlyConverter, TimeOnlyComparer>();
        }

        public async Task<IDbTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            return new DbTransaction(await base.Database.BeginTransactionAsync(cancellationToken));
        }
    }
}
