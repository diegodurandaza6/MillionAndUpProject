using Properties.Ms.AdapterOutRepository.SqlServer.Entities;
using Properties.Ms.Domain.Property.Models;

namespace Properties.Ms.AdapterOutRepository.SqlServer.Mappers
{
    public static class PropertyMapper
    {

        /// <summary>
        /// Mapea un objeto PropertyEntity a Property
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>Objeto Property</returns>
        public static Property ToDomain(this PropertyEntity entity)
        {
            return new Property
            {
                IdProperty = entity.IdProperty,
                Name = entity.Name,
                Address = entity.Address,
                Price = entity.Price,
                CodeInternal = entity.CodeInternal,
                Year = entity.Year,
                IdOwner = entity.IdOwner
            };
        }

        /// <summary>
        /// Mapea un objeto Property a PropertyEntity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>Objeto PropertyEntity</returns>

        public static PropertyEntity ToEntity(this Property property)
        {
            return new PropertyEntity
            {
                IdProperty = property.IdProperty,
                Name = property.Name,
                Address = property.Address,
                Price = property.Price,
                CodeInternal = property.CodeInternal,
                Year = property.Year,
                IdOwner = property.IdOwner
            };
        }

        /// <summary>
        /// Consulta la entidad PropertyEntity
        /// </summary>
        /// <param name="enumerableEntity"></param>
        /// <returns>Enumeración iterable de la entidad Property</returns>
        public static IEnumerable<Property> ToDomainIterable(this IEnumerable<PropertyEntity> enumerableEntity)
        {
            return enumerableEntity.Select(x => x.ToDomain());
        }
    }
}
