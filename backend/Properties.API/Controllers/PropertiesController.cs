using Microsoft.AspNetCore.Mvc;
using Properties.Application.Dtos;
using Properties.Application.Services;

namespace Properties.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertyController : ControllerBase
    {
        private readonly PropertyService _service;

        public PropertyController(PropertyService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string? name, [FromQuery] string? address, [FromQuery] decimal? minPrice, [FromQuery] decimal? maxPrice)
        {
            var result = await _service.GetPropertiesAsync(name, address, minPrice, maxPrice);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await _service.GetPropertyByIdAsync(id);

            if (result == null)
            {
                return NotFound($"No se encontró una propiedad con el ID: {id}");
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PropertyCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _service.AddPropertyAsync(dto);
            return Ok();
        }
    }
}
