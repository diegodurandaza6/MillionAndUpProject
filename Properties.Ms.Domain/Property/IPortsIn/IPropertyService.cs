using Properties.Ms.Domain.Property.Models;

namespace Properties.Ms.Domain.Property.IPortsIn
{
    public interface IPropertyService
    {
        Models.Property CreatePropertyBuilding(PropertyRequest propertyRequest);
        Task<PropertyImage> AddImageFromProperty(PropertyImageRequest imageRequest);
        Task<int> ChangePriceFromProperty(int idProperty, ChangePricePropertyRequest price);
        Task<PropertyResponse> UpdateProperty(int idProperty, PropertyRequest propertyRequest);
        Task<IEnumerable<Models.Property>> ListPropertyWithFilters(string? address, decimal? price, int? year);
    }
}
