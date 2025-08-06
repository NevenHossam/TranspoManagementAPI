using Microsoft.AspNetCore.Mvc;
using TranspoManagementAPI.DTO;
using TranspoManagementAPI.Services.Interfaces;

namespace TranspoManagementAPI.Controllers
{
    /// <summary>
    /// Controller for managing fare bands (rate per mile by distance).
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class FareBandController : ControllerBase
    {
        private readonly IFareBandService _service;
        /// <summary>
        /// constructor of FareBandController
        /// </summary>
        /// <param name="service">IFareBandService obj injection</param>
        public FareBandController(IFareBandService service)
        {
            _service = service;
        }

        /// <summary>
        /// Get all fare bands ordered by distance limit.
        /// </summary>
        /// <returns>List of FareBandResponseDto</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FareBandResponseDto>>> GetAll()
        {
            var bands = await _service.GetAllOrderedAsync();
            return Ok(bands);
        }

        /// <summary>
        /// Get a fare band by its ID.
        /// </summary>
        /// <param name="id">Fare band ID</param>
        /// <returns>FareBandResponseDto</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<FareBandResponseDto>> GetById(int id)
        {
            var band = await _service.GetByIdAsync(id);
            if (band == null) return NotFound();
            return Ok(band);
        }

        /// <summary>
        /// Create a new fare band.
        /// </summary>
        /// <param name="request">Fare band creation request</param>
        /// <returns>Created FareBandResponseDto</returns>
        [HttpPost]
        public async Task<ActionResult<FareBandResponseDto>> Create([FromBody] FareBandRequestDto request)
        {
            var band = await _service.CreateAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = band.Id }, band);
        }

        /// <summary>
        /// Update an existing fare band.
        /// </summary>
        /// <param name="id">Fare band ID</param>
        /// <param name="request">Fare band update request</param>
        /// <returns>No content if successful</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] FareBandRequestDto request)
        {
            var success = await _service.UpdateAsync(id, request);
            if (!success) return NotFound();
            return NoContent();
        }

        /// <summary>
        /// Delete a fare band by its ID.
        /// </summary>
        /// <param name="id">Fare band ID</param>
        /// <returns>No content if successful, NotFound if not found</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _service.DeleteAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}
