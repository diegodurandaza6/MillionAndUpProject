using Microsoft.EntityFrameworkCore;
using Properties.Ms.AdapterOutRepository.SqlServer.Entities;
using Properties.Ms.AdapterOutRepository.SqlServer.Mappers;
using Properties.Ms.Domain.Property.IPortsOut;
using Properties.Ms.Domain.Property.Models;
using System;
using System.Linq;
using System.Xml;

namespace Properties.Ms.AdapterOutRepository.SqlServer.Repositories
{
    public class PropertyRepository : IPropertyRepository
    {
        private readonly PropertyDBContext _DbContext;

        public PropertyRepository(PropertyDBContext DbContext)
        {
            _DbContext = DbContext;
        }

        public Property CreatePropertyBuilding(Property property)
        {
            using var context = _DbContext;
            PropertyEntity newPropertyEntity = context.Property.Add(property.ToEntity()).Entity;
            _DbContext.SaveChanges();
            return newPropertyEntity.ToDomain();
        }

        public PropertyImage AddImageFromProperty(PropertyImage image)
        {
            PropertyImageEntity newImagePropertyEntity = _DbContext.PropertyImage.Add(image.ToEntity()).Entity;
            _DbContext.SaveChanges();
            return newImagePropertyEntity.ToDomain();
        }

        public async Task<int> ChangePriceFromProperty(int idProperty, ChangePricePropertyRequest request)
        {
            PropertyEntity? property = await _DbContext.Property.FindAsync(idProperty);

            if (property != null)
            {
                property.Price = request.Price;
                _DbContext.Entry(property).State = EntityState.Modified;
                return await _DbContext.SaveChangesAsync();
            }else
            {
                return 0;
            }
        }

        public async Task<PropertyResponse> UpdateProperty(int idProperty, PropertyRequest request)
        {
            PropertyEntity? property = await _DbContext.Property.FindAsync(idProperty);
            PropertyResponse response = new();            
            if (property != null)
            {
                property.Name = request.Name;
                property.Address = request.Address;
                property.Price = request.Price;
                property.Year = request.Year;
                property.IdOwner = request.IdOwner;
                _DbContext.Entry(property).State = EntityState.Modified;
                int modifiedEntitiesCount = await _DbContext.SaveChangesAsync();
                if (modifiedEntitiesCount >= 1)
                {
                    response.Message = $"Properties ({modifiedEntitiesCount}) updated correctly.";
                    response.code = 1;
                }
                else
                {
                    response.Message = "An error has occurred.";
                    response.code = 0;
                }
            }
            else
            {
                response.Message = "Property not found.";
                response.code = -1;
            }
            return response;
        }

        public async Task<IEnumerable<Property>> ListPropertyWithFilters(string? address, decimal? price, int? year)
        {
            IQueryable<PropertyEntity> query = _DbContext.Property.AsQueryable<PropertyEntity>();
            if (!string.IsNullOrEmpty(address))
            {
                query = query.Where(x => x.Address == address);
            }
            if (price.HasValue)
            {
                query = query.Where(x => x.Price <= price.Value);
            }
            if (year.HasValue)
            {
                query = query.Where(x => x.Year == year.Value);
            }
            List<PropertyEntity> result = await query.ToListAsync();

            return result.ToDomainIterable();
        }
    }
}
