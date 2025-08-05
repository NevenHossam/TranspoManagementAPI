using Microsoft.AspNetCore.Mvc;
using TranspoManagementAPI.DTO;
using TranspoManagementAPI.Services.Interfaces;

namespace TranspoManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FareBandController : ControllerBase
    {
        private readonly IFareBandService _service;
        public FareBandController(IFareBandService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FareBandResponseDto>>> GetAll()
        {
            var bands = await _service.GetAllOrderedAsync();
            return Ok(bands);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FareBandResponseDto>> GetById(int id)
        {
            var band = await _service.GetByIdAsync(id);
            if (band == null) return NotFound();
            return Ok(band);
        }

        [HttpPost]
        public async Task<ActionResult<FareBandResponseDto>> Create([FromBody] FareBandRequestDto request)
        {
            var band = await _service.CreateAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = band.Id }, band);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] FareBandRequestDto request)
        {
            var success = await _service.UpdateAsync(id, request);
            if (!success) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _service.DeleteAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}
