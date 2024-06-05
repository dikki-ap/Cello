using Cello.Application.Repositories;
using Cello.Infrastructure.Common.Context;
using Cello.Infrastructure.Common.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cello.Infrastructure.MySql
{
    public static class ServiceExtensions
    {
        public static void ConfigurePersistence(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("Cello");
            services.AddDbContext<ICelloDbContext, CelloDbContext>(opt => opt.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString),
                options => options.MigrationsAssembly(typeof(ServiceExtensions).Assembly.GetName().Name)));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserRepository, UserRepository>();
        }
    }
}
