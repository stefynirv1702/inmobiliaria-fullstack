using Microsoft.AspNetCore.Mvc;
using Properties.Application.Dtos;
using Properties.Application.Services;

namespace Properties.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OwnersController : ControllerBase
    {
        private readonly OwnerService _ownerService;

        public OwnersController(OwnerService ownerService)
        {
            _ownerService = ownerService;
        }

        /// <summary>
        /// Lista todos los owners
        /// </summary>
        [HttpGet(Name = "Get")]
        public async Task<IActionResult> Get()
        {
            var owners = await _ownerService.GetOwnersAsync();
            return Ok(owners);
        }


        /// <summary>
        /// Crea un nuevo owner
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] OwnerDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _ownerService.AddOwnerAsync(dto);
            return Ok();
        }
    }
}
