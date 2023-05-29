using Properties.Ms.AdapterInHttp.Controllers.Version1;
using Moq;
using Properties.Ms.Domain.Property.IPortsIn;
using Microsoft.AspNetCore.Mvc;
using Properties.Ms.Domain.Property.Models;
using Microsoft.Extensions.Logging;

namespace Properties.Ms.AdapterInHttp.UnitTest
{
    internal class PropertyControllerUnitTest
    {
        private PropertyController _controller;
        private Mock<IPropertyService> _requestMock;
        private Mock<ILogger<PropertyController>> _logger;
        private PropertyRequest mockRequest;
        
        [SetUp]
        public void Setup()
        {
            _requestMock = new Mock<IPropertyService>();
            _logger = new Mock<ILogger<PropertyController>>();
            _controller = new PropertyController(_requestMock.Object, _logger.Object);
            mockRequest = new() { Name = "Property name 5", Address = "Property address 5", Price = 5000000, Year = 2020, IdOwner = 5 };
        }

        [Test]
        public void GetAllPropertiesTestOkResult()
        {
            var response = _controller.GetPropertyList(null, null, null);
            Assert.That(response, Is.InstanceOf<Task<IActionResult>>());
        }

        [Test]
        public async Task UpdatePropertyTestOkResult()
        {
            var response = await _controller.UpdateProperty(1, mockRequest);
            Assert.That(response, Is.InstanceOf<OkObjectResult>());
        }
    }
}
