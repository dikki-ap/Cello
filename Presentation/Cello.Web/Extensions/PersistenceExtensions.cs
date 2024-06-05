namespace Cello.Web.Extensions
{
    public static class PersistenceExtensions
    {
        public static void ConfigurePersistence(this IServiceCollection services, IConfiguration configuration)
        {
            var databaseProvider = configuration.GetValue<string>("PersistenceProvider");
            if(databaseProvider != null && string.Equals(databaseProvider, typeof(Cello.Infrastructure.MySql.ServiceExtensions).Assembly.GetName().Name))
            {
                Cello.Infrastructure.MySql.ServiceExtensions.ConfigurePersistence(services, configuration);
            }
            else
            {
                throw new Exception("Invalid database provider: " + databaseProvider);
            }
        }
    }
}
