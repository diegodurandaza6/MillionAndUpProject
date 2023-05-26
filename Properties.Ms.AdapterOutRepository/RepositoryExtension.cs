using Microsoft.Extensions.DependencyInjection;
using Properties.Ms.AdapterOutRepository.SqlServer.Repositories;
using Properties.Ms.Domain.Property.IPortsOut;

namespace Properties.Ms.AdapterOutRepository
{
    public static class RepositoryExtension
    {
        public static IServiceCollection AddRepository(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IPropertyRepository, PropertyRepository>();
            return serviceCollection;
        }
    }
}
