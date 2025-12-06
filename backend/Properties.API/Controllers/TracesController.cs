using Microsoft.AspNetCore.Mvc;
using Properties.Application.Dtos;
using Properties.Application.Services;

namespace Properties.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TraceController : ControllerBase
    {
        private readonly TraceService _traceService;

        public TraceController(TraceService traceService)
        {
            _traceService = traceService;
        }


        /// <summary>
        /// Crea un nuevo trace
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PropertyTraceCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _traceService.AddPropertyTraceAsync(dto);
            return Ok();
        }
    }
}
