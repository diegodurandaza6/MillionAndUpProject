using Microsoft.EntityFrameworkCore;
using Properties.Ms.AdapterOutRepository.SqlServer;
using Properties.Ms.AdapterOutRepository.SqlServer.Entities;
using Properties.Ms.AdapterOutRepository.UnitTest.Common;

namespace Properties.Ms.AdapterOutRepository.UnitTest.Context
{
    internal class DbContextUnitTest
    {
        private PropertyDBContext _context;

        [SetUp]
        public void Init()
        {
            _context = DbContextFactory.Create();
        }

        [TearDown]
        public void Cleanup()
        {
            DbContextFactory.Destroy(_context);
        }

        [Test]
        public void CanCreatePropertyIntoDatabase()
        {
            PropertyEntity propertyEntity = new() { IdProperty = 4, Name = "Property name 4", Address = "Property address 4", Price = 4000000, CodeInternal = "123456789", Year = 2023, IdOwner = 1 };
            _context.Property.Add(propertyEntity);
            Assert.That(_context.Entry(propertyEntity).State, Is.EqualTo(EntityState.Added));

            var result = _context.SaveChangesAsync();
            Assert.Multiple(() =>
            {
                Assert.That(result.Result, Is.EqualTo(1));
                Assert.That(result.Status, Is.EqualTo(Task.CompletedTask.Status));
            });
            Assert.That(_context.Entry(propertyEntity).State, Is.EqualTo(EntityState.Unchanged));
        }

        [Test]
        public void CanUpdatePropertyIntoDatabase()
        {
            PropertyEntity? propertyEntity = _context.Property.Find(1);//Find(p => p.IdProperty == 1);
            propertyEntity!.Price = 1500000;
            _context.Property.Update(propertyEntity);
            Assert.That(_context.Entry(propertyEntity).State, Is.EqualTo(EntityState.Modified));

            var result = _context.SaveChangesAsync();
            Assert.Multiple(() =>
            {
                Assert.That(result.Result, Is.EqualTo(1));
                Assert.That(result.Status, Is.EqualTo(Task.CompletedTask.Status));
            });
            Assert.That(_context.Entry(propertyEntity).State, Is.EqualTo(EntityState.Unchanged));
        }
    }
}
