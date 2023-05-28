using Microsoft.Extensions.DependencyInjection;
using Properties.Ms.Domain.Property.IPortsIn;

namespace Properties.Ms.Application
{
    public static class ApplicationExtension
    {
        public static IServiceCollection AddApplication(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IPropertyService, PropertyService>();
            return serviceCollection;
        }
    }
}
