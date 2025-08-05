using Microsoft.AspNetCore.Mvc;
using TranspoManagementAPI.DTO;
using TranspoManagementAPI.Services.Interfaces;
using TranspoManagementAPI.Models;

[ApiController]
[Route("api/[controller]")]
public class VehicleController : ControllerBase
{
    private readonly IVehicleService _vehicleService;

    public VehicleController(IVehicleService vehicleService)
    {
        _vehicleService = vehicleService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<VehicleResponseDto>>> GetAll()
    {
        var vehicles = await _vehicleService.GetAllAsync();
        return Ok(vehicles);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<VehicleResponseDto>> GetById(int id)
    {
        var vehicle = await _vehicleService.GetByIdAsync(id);
        if (vehicle == null)
            return NotFound();
        return Ok(vehicle);
    }

    [HttpPost]
    public async Task<ActionResult<VehicleResponseDto>> Create([FromBody] VehicleRequest request)
    {
        var vehicle = await _vehicleService.CreateAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = vehicle.Id }, vehicle);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] VehicleRequest request)
    {
        var success = await _vehicleService.UpdateAsync(id, request);
        if (!success)
            return NotFound();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _vehicleService.DeleteAsync(id);
        if (!success)
            return NotFound();
        return NoContent();
    }
}
