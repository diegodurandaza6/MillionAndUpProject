using Microsoft.EntityFrameworkCore;
using Properties.Ms.AdapterOutRepository.SqlServer;
using Properties.Ms.AdapterOutRepository.SqlServer.Entities;
using Properties.Ms.Domain.Property.Models;

namespace Properties.Ms.AdapterOutRepository.UnitTest.Common
{
    internal class DbContextFactory
    {
        public static List<PropertyEntity> GetProperties()
        {
            return new List<PropertyEntity>()
            {
                new(){ IdProperty=1, Name="Property name 1", Address = "Property address 1", Price = 1000000, CodeInternal = "123456789", Year = 2023, IdOwner = 1 },
                new(){ IdProperty=2, Name="Property name 2", Address = "Property address 2", Price = 2000000, CodeInternal = "123456789", Year = 2023, IdOwner = 1 },
                new(){ IdProperty=3, Name="Property name 3", Address = "Property address 3", Price = 3000000, CodeInternal = "123456789", Year = 2023, IdOwner = 1 },
            };
        }

        public static PropertyDBContext Create()
        {
            var options = new DbContextOptionsBuilder<PropertyDBContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            PropertyDBContext context = new(options);
            context.Database.EnsureCreated();
            context.Property.AddRange(GetProperties());
            context.SaveChanges();
            return context;
        }
        public static void Destroy(PropertyDBContext dbContext)
        {
            dbContext.Database.EnsureDeleted();
            dbContext.Dispose();
        }
    }
}
