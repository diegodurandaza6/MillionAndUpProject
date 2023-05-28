using Properties.Ms.Domain.Property.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Properties.Ms.Domain.UnitTest
{
    internal class PropertyUnitTest
    {
        private readonly Property.Models.Property _property;
        private const int IdProperty = 1;
        private const string Name = "Property name";
        private const string Address = "Property address";
        private const decimal Price = 1000000;
        private const string CodeInternal = "123456789";
        private const short Year = 2023;
        private const int IdOwner = 1;

        public PropertyUnitTest(){
            _property = new Property.Models.Property();
        }

        [Test]
        public void TestSetAndGetProperties()
        {
            _property.IdProperty = IdProperty;
            _property.Name = Name;
            _property.Address = Address;
            _property.Price = Price;
            _property.CodeInternal = CodeInternal;
            _property.Year = Year;
            _property.IdOwner = IdOwner;
            Assert.Multiple(() =>
            {
                Assert.That(_property.IdProperty, Is.EqualTo(IdProperty));
                Assert.That(_property.Name, Is.EqualTo(Name));
                Assert.That(_property.Address, Is.EqualTo(Address));
                Assert.That(_property.Price, Is.EqualTo(Price));
                Assert.That(_property.CodeInternal, Is.EqualTo(CodeInternal));
                Assert.That(_property.Year, Is.EqualTo(Year));
                Assert.That(_property.IdOwner, Is.EqualTo(IdOwner));
            });
        }
    }
}
