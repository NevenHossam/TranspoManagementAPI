using Microsoft.AspNetCore.Mvc;
using TranspoManagementAPI.DTO;
using TranspoManagementAPI.Services.Interfaces;
using TranspoManagementAPI.Models;

[ApiController]
[Route("api/[controller]")]
public class TripController : ControllerBase
    /// <summary>
    /// Controller for managing trips.
    /// </summary>
{
    private readonly ITripService _tripService;

    public TripController(ITripService tripService)
    {
        _tripService = tripService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TripResponseDto>>> GetAll()
    /// <summary>
    /// Get all trips.
    /// </summary>
    /// <returns>List of TripResponseDto</returns>
    {
        var trips = await _tripService.GetAllAsync();
        return Ok(trips);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TripResponseDto>> GetById(int id)
    /// <summary>
    /// Get a trip by its ID.
    /// </summary>
    /// <param name="id">Trip ID</param>
    /// <returns>TripResponseDto</returns>
    {
        var trip = await _tripService.GetByIdAsync(id);
        if (trip == null)
            return NotFound();
        return Ok(trip);
    }

    [HttpPost]
    public async Task<ActionResult<TripResponseDto>> Create([FromBody] TripRequest request)
    /// <summary>
    /// Create a new trip.
    /// </summary>
    /// <param name="request">Trip creation request</param>
    /// <returns>Created TripResponseDto</returns>
    {
        if (request.Distance <= 0)
            return BadRequest("Distance must be greater than 0.");
        var trip = await _tripService.CreateAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = trip.Id }, trip);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] TripRequest request)
    /// <summary>
    /// Update a specific trip.
    /// </summary>
    /// <param name="request">Trip request</param>
    /// <returns>No content if successful</returns>
    {
        var success = await _tripService.UpdateAsync(id, request);
        if (!success)
            return NotFound();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    /// <summary>
    /// Delete a specific trip.
    /// </summary>
    /// <param name="request">Trip ID</param>
    /// <returns>No content if successful</returns>
    {
        var success = await _tripService.DeleteAsync(id);
        if (!success)
            return NotFound();
        return NoContent();
    }

    [HttpGet("vehicle/{vehicleId}")]
    public async Task<ActionResult<IEnumerable<TripResponseDto>>> GetTripsByVehicle(int vehicleId)
    /// <summary>
    /// Get all trips of a specific vehicle.
    /// </summary>
    /// <param name="request">Vehicle ID</param>
    /// <returns>List of TripResponseDto</returns>
    {
        var trips = await _tripService.GetTripsByVehicleAsync(vehicleId);
        return Ok(trips);
    }

    [HttpGet("vehicle/{vehicleId}/trip/{tripId}")]
    public async Task<ActionResult<TripResponseDto>> GetTripByVehicle(int vehicleId, int tripId)
    /// <summary>
    /// Get a trip by trip ID and vehicle ID.
    /// </summary>
    /// <param name="request">Vehicle ID</param>
    /// <returns>TripResponseDto</returns>
    {
        var trip = await _tripService.GetTripByVehicleAsync(vehicleId, tripId);
        if (trip == null)
            return NotFound();
        return Ok(trip);
    }
}
