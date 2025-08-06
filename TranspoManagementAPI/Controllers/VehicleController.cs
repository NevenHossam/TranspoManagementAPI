using Microsoft.AspNetCore.Mvc;
using TranspoManagementAPI.DTO;
using TranspoManagementAPI.Services.Interfaces;

/// <summary>
/// Controller for managing vehicles.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class VehicleController : ControllerBase
{
    private readonly IVehicleService _vehicleService;

    /// <summary>
    /// ctor of VehicleController
    /// </summary>
    /// <param name="vehicleService">IVehicleService obj injection</param>
    public VehicleController(IVehicleService vehicleService)
    {
        _vehicleService = vehicleService;
    }

    /// <summary>
    /// Get all vehicles.
    /// </summary>
    /// <returns>List of VehicleResponseDto</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<VehicleResponseDto>>> GetAll()
    {
        var vehicles = await _vehicleService.GetAllAsync();
        return Ok(vehicles);
    }

    /// <summary>
    /// Get a vehicle by its ID.
    /// </summary>
    /// <param name="id">Vehicle ID</param>
    /// <returns>VehicleResponseDto</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<VehicleResponseDto>> GetById(int id)
    {
        var vehicle = await _vehicleService.GetByIdAsync(id);
        if (vehicle == null)
            return NotFound();
        return Ok(vehicle);
    }

    /// <summary>
    /// Create a new vehicle.
    /// </summary>
    /// <param name="request">Vehicle creation request</param>
    /// <returns>Created VehicleResponseDto</returns>
    [HttpPost]
    public async Task<ActionResult<VehicleResponseDto>> Create([FromBody] VehicleRequestDto request)
    {
        var vehicle = await _vehicleService.CreateAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = vehicle.Id }, vehicle);
    }

    /// <summary>
    /// Update a specific vehicle.
    /// </summary>
    /// <param name="id">Vehicle ID</param>
    /// <param name="request">Vehicle request</param>
    /// <returns>No content if successful</returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] VehicleRequestDto request)
    {
        var success = await _vehicleService.UpdateAsync(id, request);
        if (!success)
            return NotFound();
        return NoContent();
    }

    /// <summary>
    /// Delete a specific vehicle.
    /// </summary>
    /// <param name="id">Vehicle ID</param>
    /// <returns>No content if successful</returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _vehicleService.DeleteAsync(id);
        if (!success)
            return NotFound();
        return NoContent();
    }
}
