using Microsoft.Extensions.DependencyInjection;
using Properties.Ms.Domain.Property;
using Properties.Ms.Domain.Property.IPortsIn;

namespace Properties.Ms.Domain
{
    public static class DomainExtension
    {
        public static IServiceCollection AddDomain(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IPropertyService, PropertyService>();
            return serviceCollection;
        }
    }
}
