using Microsoft.AspNetCore.Mvc;
using TranspoManagementAPI.DTO;
using TranspoManagementAPI.Services.Interfaces;

[ApiController]
[Route("api/[controller]")]
public class VehicleController : ControllerBase
    /// <summary>
    /// Controller for managing vehicles.
    /// </summary>
{
    private readonly IVehicleService _vehicleService;

    public VehicleController(IVehicleService vehicleService)
    {
        _vehicleService = vehicleService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<VehicleResponseDto>>> GetAll()
    /// <summary>
    /// Get all vehicles.
    /// </summary>
    /// <returns>List of VehicleResponseDto</returns>
    {
        var vehicles = await _vehicleService.GetAllAsync();
        return Ok(vehicles);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<VehicleResponseDto>> GetById(int id)
    /// <summary>
    /// Get a vehicle by its ID.
    /// </summary>
    /// <param name="id">Vehicle ID</param>
    /// <returns>VehicleResponseDto</returns>
    {
        var vehicle = await _vehicleService.GetByIdAsync(id);
        if (vehicle == null)
            return NotFound();
        return Ok(vehicle);
    }

    [HttpPost]
    public async Task<ActionResult<VehicleResponseDto>> Create([FromBody] VehicleRequest request)
    /// <summary>
    /// Create a new vehicle.
    /// </summary>
    /// <param name="request">Vehicle creation request</param>
    /// <returns>Created VehicleResponseDto</returns>
    {
        var vehicle = await _vehicleService.CreateAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = vehicle.Id }, vehicle);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] VehicleRequest request)
    /// <summary>
    /// Update a specific vehicle.
    /// </summary>
    /// <param name="request">Vehicle request</param>
    /// <returns>No content if successful</returns>
    {
        var success = await _vehicleService.UpdateAsync(id, request);
        if (!success)
            return NotFound();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    /// <summary>
    /// Delete a specific vehicle.
    /// </summary>
    /// <param name="request">Vehicle ID</param>
    /// <returns>No content if successful</returns>
    {
        var success = await _vehicleService.DeleteAsync(id);
        if (!success)
            return NotFound();
        return NoContent();
    }
}
