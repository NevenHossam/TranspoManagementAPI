using TranspoManagementAPI.DTO;
using TranspoManagementAPI.IServices;
using Microsoft.AspNetCore.Mvc;

namespace TranspoManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    /// <summary>
    /// Controller for fare calculation operations.
    /// </summary>
    public class FareController : ControllerBase
    {
        private readonly IFareCalcService _fareCalculator;

        public FareController(IFareCalcService fareCalculator)
        {
            _fareCalculator = fareCalculator;
        }
        [HttpPost("calculate")]
        public async Task<ActionResult> CalculateFare([FromBody] FareCalcRequest request)
        /// <summary>
        /// Calculate fare for a given distance.
        /// </summary>
        /// <param name="request">Fare calculation request</param>
        /// <returns>Calculated fare</returns>
        {
            if (request == null || request.Distance < 0)
                return BadRequest("Invalid distance input.");

            double fare = await _fareCalculator.CalculateFare(request.Distance);
            return Ok(new { fare });
        }
    }
}

