using Microsoft.AspNetCore.Mvc;
using TranspoManagementAPI.DTO;
using TranspoManagementAPI.Services.Interfaces;

namespace TranspoManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    /// <summary>
    /// Controller for managing fare bands (rate per mile by distance).
    /// </summary>
    public class FareBandController : ControllerBase
    {
        private readonly IFareBandService _service;
        public FareBandController(IFareBandService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FareBandResponseDto>>> GetAll()
        /// <summary>
        /// Get all fare bands ordered by distance limit.
        /// </summary>
        /// <returns>List of FareBandResponseDto</returns>
        {
            var bands = await _service.GetAllOrderedAsync();
            return Ok(bands);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FareBandResponseDto>> GetById(int id)
        /// <summary>
        /// Get a fare band by its ID.
        /// </summary>
        /// <param name="id">Fare band ID</param>
        /// <returns>FareBandResponseDto</returns>
        {
            var band = await _service.GetByIdAsync(id);
            if (band == null) return NotFound();
            return Ok(band);
        }

        [HttpPost]
        public async Task<ActionResult<FareBandResponseDto>> Create([FromBody] FareBandRequestDto request)
        /// <summary>
        /// Create a new fare band.
        /// </summary>
        /// <param name="request">Fare band creation request</param>
        /// <returns>Created FareBandResponseDto</returns>
        {
            var band = await _service.CreateAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = band.Id }, band);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] FareBandRequestDto request)
        /// <summary>
        /// Update an existing fare band.
        /// </summary>
        /// <param name="id">Fare band ID</param>
        /// <param name="request">Fare band update request</param>
        /// <returns>No content if successful</returns>
        {
            var success = await _service.UpdateAsync(id, request);
            if (!success) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        /// <summary>
        /// Delete a fare band by its ID.
        /// </summary>
        /// <param name="id">Fare band ID</param>
        /// <returns>No content if successful, NotFound if not found</returns>
        {
            var success = await _service.DeleteAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}
