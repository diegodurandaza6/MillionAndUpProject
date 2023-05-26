using Properties.Ms.AdapterOutRepository.SqlServer.Entities;
using Properties.Ms.Domain.Property.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Properties.Ms.AdapterOutRepository.SqlServer.Mappers
{
    public static class PropertyImageMapper
    {
        public static PropertyImage ToDomain(this PropertyImageEntity entity)
        {
            return new PropertyImage
            {
                IdPropertyImage = entity.IdPropertyImage,
                IdProperty = entity.IdProperty,
                File = entity.File,
                Enabled = entity.Enabled
            };
        }

        public static PropertyImageEntity ToEntity(this PropertyImage property)
        {
            return new PropertyImageEntity
            {
                IdProperty = property.IdProperty,
                IdPropertyImage = property.IdPropertyImage,
                File = property.File,
                Enabled = property.Enabled
            };
        }
    }
}
