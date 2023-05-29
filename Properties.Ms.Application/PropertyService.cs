﻿using Properties.Ms.Domain.Property.Models;
using Properties.Ms.Domain.Property.IPortsIn;
using Properties.Ms.Domain.Property.IPortsOut;

namespace Properties.Ms.Application
{
    public class PropertyService : IPropertyService
    {
        private readonly IPropertyRepository _propertyRepository;
        public PropertyService(IPropertyRepository propertyRepository)
        {
            _propertyRepository = propertyRepository;
        }

        /// <summary>
        /// Crea una propiedad en el contexto
        /// </summary>
        /// <param name="property"></param>
        /// <returns>Retorna la entidad creada</returns>
        public Property CreatePropertyBuilding(PropertyRequest request)
        {
            Property property = new()
            {
                Name = request.Name,
                Address = request.Address,
                CodeInternal = Guid.NewGuid().ToString(),
                Price = request.Price,
                Year = request.Year,
                IdOwner = request.IdOwner,
            };
            return _propertyRepository.CreatePropertyBuilding(property);
        }

        /// <summary>
        /// Almacena la ruta creada en donde se almacena el archivo
        /// </summary>
        /// <param name="image"></param>
        /// <returns>La ruta de la imagen</returns>
        public async Task<PropertyImage> AddImageFromProperty(PropertyImageRequest request)
        {
            PropertyImage propertyImage = new()
            {
                IdProperty = request.IdProperty,
                File = await GetImagePath(request.FileBase64, request.mimeType),
                Enabled = true,
            };
            return _propertyRepository.AddImageFromProperty(propertyImage);
        }

        /// <summary>
        /// Permite almacenar el archivo base 64 en la ubicación especificada
        /// </summary>
        /// <param name="imageBase64"></param>
        /// <param name="mimeType"></param>
        /// <returns>El path del archivo almacenado</returns>
        private static async Task<string> GetImagePath(string imageBase64, string mimeType)
        {
            byte[] image = Convert.FromBase64String(imageBase64);
            string directory = string.Format("{0}\\{1}", Directory.GetCurrentDirectory(), "FileServer\\PropertiesImages");
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            var newFileName = Guid.NewGuid();
            string extension = Path.GetExtension(mimeType);
            string WriteImageDirectoryPath = string.Format("{0}\\{1}{2}", directory, newFileName, extension);
            if (!File.Exists(WriteImageDirectoryPath))
            {
                using FileStream imageFile = new(WriteImageDirectoryPath, FileMode.Create);
                await imageFile.WriteAsync(image);
                imageFile.Flush();
                imageFile.Dispose();
            }
            return WriteImageDirectoryPath;
        }

        /// <summary>
        /// Permite cambiar el precio de una propiedad determinada
        /// </summary>
        /// <param name="idProperty"></param>
        /// <param name="request"></param>
        /// <returns>Retorna la cantidad de registros modificados</returns>
        public async Task<int> ChangePriceFromProperty(int idProperty, ChangePricePropertyRequest request)
        {
            return await _propertyRepository.ChangePriceFromProperty(idProperty, request);
        }

        /// <summary>
        /// Actualiza la información de una propiedad determinada
        /// </summary>
        /// <param name="idProperty"></param>
        /// <param name="request"></param>
        /// <returns>La propiedad actualizada</returns>
        public Task<PropertyResponse> UpdateProperty(int idProperty, PropertyRequest request)
        {
            return _propertyRepository.UpdateProperty(idProperty, request);
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
            return await _propertyRepository.ListPropertyWithFilters(address, price, year);
        }
    }
}
