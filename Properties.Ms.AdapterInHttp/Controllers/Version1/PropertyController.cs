using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Properties.Ms.Domain.Property.IPortsIn;
using Properties.Ms.Domain.Property.Models;
using System.ComponentModel.DataAnnotations;

namespace Properties.Ms.AdapterInHttp.Controllers.Version1
{
    [ApiController]
    [ApiVersion("1.0", Deprecated = false)]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize]
    public class PropertyController : ControllerBase
    {
        private readonly IPropertyService _propertyService;

        public PropertyController(IPropertyService propertyService)
        {
            _propertyService = propertyService;
        }

        [HttpPost("Create")]
        public IActionResult CreatePropertyBuilding([FromBody, Required] PropertyRequest propertyRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Property newProperty = _propertyService.CreatePropertyBuilding(propertyRequest);
            return Created("", newProperty);
            //return Ok(newProperty);
        }

        [HttpPost("AddImage")]
        public async Task<IActionResult> AddImageFromProperty([FromBody, Required] PropertyImageRequest propertyImageRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            PropertyImage newImageProperty = await _propertyService.AddImageFromProperty(propertyImageRequest);
            return Ok(newImageProperty);
        }

        [HttpPatch("ChangePrice/{id}")]
        [Authorize(Roles="admin")]
        public async Task<IActionResult> ChangePriceFromProperty(int id, [FromBody, Required] ChangePricePropertyRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _propertyService.ChangePriceFromProperty(id, request);
            //return Created("/buldingasd/1", newProperty);
            return Ok();
        }

        [HttpPut("Update/{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateProperty(int id, [FromBody, Required] PropertyRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            PropertyResponse response = await _propertyService.UpdateProperty(id, request);
            return Ok(response);
        }

        [HttpGet("GetPropertyList")]
        public async Task<IActionResult> GetPropertyList(string? address, decimal? price, int? year)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            IEnumerable<Property> response = await _propertyService.ListPropertyWithFilters(address, price, year);
            if (response.Any())
            {
                return Ok(response);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
