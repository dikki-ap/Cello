using Cello.Infrastructure.Common.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Cello.Infrastructure.MySql
{
    public class CelloContextFactory : IDesignTimeDbContextFactory<CelloDbContext>
    {
        public CelloDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
            .AddCommandLine(args)
            .Build();
            var builder = new DbContextOptionsBuilder<CelloDbContext>();
            var connectionString = configuration.GetSection("ConnectionString").Value;
            if (!string.IsNullOrEmpty(connectionString))
            {
                builder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString),
                    options => options.MigrationsAssembly(typeof(CelloContextFactory).Assembly.GetName().Name));
            }
            return new CelloDbContext(builder.Options);
        }
    }
}
