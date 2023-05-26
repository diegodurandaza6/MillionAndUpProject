using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Properties.Ms.AdapterOutRepository.SqlServer
{
    public static class PersistenceExtension
    {
        public static void AddPersistence(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.AddDbContext<PropertyDBContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("sqlServerDb")));
        }

        public static void MigrateDatabase(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
            using var context = serviceScope.ServiceProvider.GetService<PropertyDBContext>();
            context?.Database.Migrate();
        }
    }
}
