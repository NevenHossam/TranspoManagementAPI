using Microsoft.AspNetCore.Mvc;
using TranspoManagementAPI.DTO;
using TranspoManagementAPI.Services.Interfaces;

namespace TranspoManagementAPI.Controllers
{
    /// <summary>
    /// Controller for fare calculation operations.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class FareController : ControllerBase
    {
        private readonly IFareCalcService _fareCalculator;

        /// <summary>
        /// ctor of FareController
        /// </summary>
        /// <param name="fareCalculator">IFareCalcService obj injection</param>
        public FareController(IFareCalcService fareCalculator)
        {
            _fareCalculator = fareCalculator;
        }

        /// <summary>
        /// Calculate fare for a given distance.
        /// </summary>
        /// <param name="request">Fare calculation request</param>
        /// <returns>Calculated fare</returns>
        [HttpPost("calculate")]
        public async Task<ActionResult> CalculateFare([FromBody] FareCalcRequestDto request)
        {
            if (request == null || request.Distance < 0)
                return BadRequest("Invalid distance input.");

            double fare = await _fareCalculator.CalculateFare(request.Distance);
            return Ok(new { fare });
        }
    }
}

