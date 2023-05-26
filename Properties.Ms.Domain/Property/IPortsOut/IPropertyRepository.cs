using Properties.Ms.Domain.Property.Models;

namespace Properties.Ms.Domain.Property.IPortsOut
{
    public interface IPropertyRepository
    {
        Models.Property CreatePropertyBuilding(Models.Property property);
        PropertyImage AddImageFromProperty(PropertyImage image);
        Task<int> ChangePriceFromProperty(int idProperty, ChangePricePropertyRequest request);
        Task<PropertyResponse> UpdateProperty(int idProperty, PropertyRequest request);
        Task<IEnumerable<Models.Property>> ListPropertyWithFilters(string? address, decimal? price, int? year);
    }
}
