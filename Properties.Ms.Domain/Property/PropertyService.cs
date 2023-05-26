using Properties.Ms.Domain.Property.IPortsIn;
using Properties.Ms.Domain.Property.IPortsOut;
using Properties.Ms.Domain.Property.Models;
using System;

namespace Properties.Ms.Domain.Property
{
    public class PropertyService : IPropertyService
    {
        private readonly IPropertyRepository _propertyRepository;
        public PropertyService(IPropertyRepository propertyRepository) {
            _propertyRepository = propertyRepository;
        }

        public Models.Property CreatePropertyBuilding(PropertyRequest request)
        {
            Models.Property property = new()
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
        public async Task<int> ChangePriceFromProperty(int idProperty, ChangePricePropertyRequest request)
        {
            return await _propertyRepository.ChangePriceFromProperty(idProperty, request);
        }

        public Task<PropertyResponse> UpdateProperty(int idProperty, PropertyRequest request)
        {
            return _propertyRepository.UpdateProperty(idProperty, request);
        }

        public async Task<IEnumerable<Models.Property>> ListPropertyWithFilters(string? address, decimal? price, int? year)
        {
            return await _propertyRepository.ListPropertyWithFilters(address, price, year);
        }
    }
}
