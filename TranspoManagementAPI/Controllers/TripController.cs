using Microsoft.AspNetCore.Mvc;
using TranspoManagementAPI.DTO;
using TranspoManagementAPI.Services.Interfaces;

/// <summary>
/// Controller for managing trips.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class TripController : ControllerBase
{
    private readonly ITripService _tripService;

    /// <summary>
    /// ctor of TripController
    /// </summary>
    /// <param name="tripService">ITripService obj injection</param>
    public TripController(ITripService tripService)
    {
        _tripService = tripService;
    }

    /// <summary>
    /// Get all trips.
    /// </summary>
    /// <returns>List of TripResponseDto</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TripResponseDto>>> GetAll()
    {
        var trips = await _tripService.GetAllAsync();
        return Ok(trips);
    }

    /// <summary>
    /// Get a trip by its ID.
    /// </summary>
    /// <param name="id">Trip ID</param>
    /// <returns>TripResponseDto</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<TripResponseDto>> GetById(int id)
    {
        var trip = await _tripService.GetByIdAsync(id);
        if (trip == null)
            return NotFound();
        return Ok(trip);
    }

    /// <summary>
    /// Create a new trip.
    /// </summary>
    /// <param name="request">Trip creation request</param>
    /// <returns>Created TripResponseDto</returns>
    [HttpPost]
    public async Task<ActionResult<TripResponseDto>> Create([FromBody] TripRequestDto request)
    {
        if (request.Distance <= 0)
            return BadRequest("Distance must be greater than 0.");
        var trip = await _tripService.CreateAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = trip.Id }, trip);
    }

    /// <summary>
    /// Update a specific trip.
    /// </summary>
    /// <param name="id">Trip ID</param>
    /// <param name="request">Trip request</param>
    /// <returns>No content if successful</returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] TripRequestDto request)
    {
        var success = await _tripService.UpdateAsync(id, request);
        if (!success)
            return NotFound();
        return NoContent();
    }

    /// <summary>
    /// Delete a specific trip.
    /// </summary>
    /// <param name="id">Trip ID</param>
    /// <returns>No content if successful</returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _tripService.DeleteAsync(id);
        if (!success)
            return NotFound();
        return NoContent();
    }

    /// <summary>
    /// Get all trips of a specific vehicle.
    /// </summary>
    /// <param name="vehicleId">Vehicle ID</param>
    /// <returns>List of TripResponseDto</returns>
    [HttpGet("vehicle/{vehicleId}")]
    public async Task<ActionResult<IEnumerable<TripResponseDto>>> GetTripsByVehicle(int vehicleId)
    {
        var trips = await _tripService.GetTripsByVehicleAsync(vehicleId);
        return Ok(trips);
    }

    /// <summary>
    /// Get a trip by trip ID and vehicle ID.
    /// </summary>
    /// <param name="vehicleId">Vehicle ID</param>
    /// <param name="tripId">Trip ID</param>
    /// <returns>TripResponseDto</returns>
    [HttpGet("vehicle/{vehicleId}/trip/{tripId}")]
    public async Task<ActionResult<TripResponseDto>> GetTripByVehicle(int vehicleId, int tripId)
    {
        var trip = await _tripService.GetTripByVehicleAsync(vehicleId, tripId);
        if (trip == null)
            return NotFound();
        return Ok(trip);
    }
}
