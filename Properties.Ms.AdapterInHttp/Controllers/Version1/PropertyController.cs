using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<PropertyController> _logger;

        public PropertyController(IPropertyService propertyService,
            ILogger<PropertyController> logger)
        {
            _propertyService = propertyService;
            _logger = logger;
        }

        /// <summary>
        /// Permite crear una propiedad
        /// </summary>
        /// <param name="propertyRequest"></param>
        /// <returns>Retorna un obketo con el id y codigo interno autogenerado</returns>
        [HttpPost("Create")]
        public IActionResult CreatePropertyBuilding([FromBody, Required] PropertyRequest propertyRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                Property newProperty = _propertyService.CreatePropertyBuilding(propertyRequest);
                return Created("", newProperty);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error en CreatePropertyBuilding: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocurrió un error en el servidor");
            }
        }

        /// <summary>
        /// Permite subir imagenes de una propiedad a un servidor de archivos
        /// </summary>
        /// <param name="propertyImageRequest"></param>
        /// <returns>Retorna un objeto con la ruta del servidor en donde se guardó el archivo</returns>
        [HttpPost("AddImage")]
        public async Task<IActionResult> AddImageFromProperty([FromBody, Required] PropertyImageRequest propertyImageRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                PropertyImage newImageProperty = await _propertyService.AddImageFromProperty(propertyImageRequest);
                return Ok(newImageProperty);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error en AddImageFromProperty: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocurrió un error en el servidor");
            }
        }

        /// <summary>
        /// Permite actualizar el precio solo por parte de un usuario con rol admin
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns>No retorna algo en particular</returns>
        [HttpPatch("ChangePrice/{id}")]
        [Authorize(Roles="admin")]
        public async Task<IActionResult> ChangePriceFromProperty(int id, [FromBody, Required] ChangePricePropertyRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                await _propertyService.ChangePriceFromProperty(id, request);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error en ChangePriceFromProperty: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocurrió un error en el servidor");
            }
        }

        /// <summary>
        /// Permite actualizar todo el recurso de una propiedad solo por el rol admin
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns>Retorna el objeto de propiedad actualizado</returns>
        [HttpPut("Update/{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateProperty(int id, [FromBody, Required] PropertyRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                PropertyResponse response = await _propertyService.UpdateProperty(id, request);
                return Ok(response);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error en UpdateProperty: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocurrió un error en el servidor");
            }
        }

        /// <summary>
        /// Permite consultar las propiedades segun los filtros establecidos.
        /// </summary>
        /// <param name="address"></param>
        /// <param name="price"></param>
        /// <param name="year"></param>
        /// <returns>Retorna una lista de propiedades.</returns>
        [HttpGet("GetPropertyList")]
        public async Task<IActionResult> GetPropertyList(string? address, decimal? price, int? year)
        {
            try
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
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error en GetPropertyList: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocurrió un error en el servidor");
            }
        }
    }
}
