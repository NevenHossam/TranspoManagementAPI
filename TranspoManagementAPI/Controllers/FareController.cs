using TranspoManagementAPI.DTO;
using TranspoManagementAPI.IServices;
using Microsoft.AspNetCore.Mvc;

namespace TranspoManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FareController : ControllerBase
    {
        private readonly IFareCalcService _fareCalculator;

        public FareController(IFareCalcService fareCalculator)
        {
            _fareCalculator = fareCalculator;
        }
        [HttpPost("calculate")]
        public async Task<ActionResult> CalculateFare([FromBody] FareCalcRequest request)
        {
            if (request == null || request.Distance < 0)
                return BadRequest("Invalid distance input.");

            double fare = await _fareCalculator.CalculateFare(request.Distance);
            return Ok(new { fare });
        }
    }
}

