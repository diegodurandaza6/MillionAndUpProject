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

        /// <summary>
        /// Crea una propiedad en el contexto
        /// </summary>
        /// <param name="property"></param>
        /// <returns>Retorna la entidad creada</returns>
        public Property CreatePropertyBuilding(Property property)
        {
            using var context = _DbContext;
            PropertyEntity newPropertyEntity = context.Property.Add(property.ToEntity()).Entity;
            _DbContext.SaveChanges();
            return newPropertyEntity.ToDomain();
        }

        /// <summary>
        /// Almacena la ruta creada en donde se almacena el archivo
        /// </summary>
        /// <param name="image"></param>
        /// <returns>La ruta de la imagen</returns>
        public PropertyImage AddImageFromProperty(PropertyImage image)
        {
            PropertyImageEntity newImagePropertyEntity = _DbContext.PropertyImage.Add(image.ToEntity()).Entity;
            _DbContext.SaveChanges();
            return newImagePropertyEntity.ToDomain();
        }

        /// <summary>
        /// Permite cambiar el precio de una propiedad determinada
        /// </summary>
        /// <param name="idProperty"></param>
        /// <param name="request"></param>
        /// <returns>Retorna la cantidad de registros modificados</returns>
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

        /// <summary>
        /// Actualiza la información de una propiedad determinada
        /// </summary>
        /// <param name="idProperty"></param>
        /// <param name="request"></param>
        /// <returns>La propiedad actualizada</returns>
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

        /// <summary>
        /// Consulta la lista de propiedades por diferentes filtros
        /// </summary>
        /// <param name="address"></param>
        /// <param name="price"></param>
        /// <param name="year"></param>
        /// <returns>Enumeración de propiedades.</returns>
        public async Task<IEnumerable<Property>> ListPropertyWithFilters(string? address, decimal? price, int? year)
        {
            try
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
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
