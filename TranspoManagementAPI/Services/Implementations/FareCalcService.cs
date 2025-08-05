using System.Threading.Tasks;
using TranspoManagementAPI.IServices;
using TranspoManagementAPI.Models;
using TranspoManagementAPI.Services.Interfaces;

namespace TranspoManagementAPI.Services
{
public class FareCalcService : IFareCalcService
    {
        private readonly IFareBandService _fareBands;

        public FareCalcService(IFareBandService fareBands)
        {
            _fareBands = fareBands;
        }

        public async Task<double> CalculateFare(double distance, double multiplier = 1.0)
        {
            double fare = 0;
            double previousLimit = 0;
            var bands = await _fareBands.GetAllOrderedAsync();

            if (bands.Any(b => b.RatePerMile <= 0))
                throw new Exception("Invalid band configuration: Rates must be positive.");

            if (!bands.SequenceEqual(bands.OrderBy(b => b.DistanceLimit ?? double.MaxValue)))
                throw new Exception("Bands must be sorted by DistanceLimit.");

            foreach (var band in bands)
            {
                double currentLimit = band.DistanceLimit ?? double.MaxValue;

                if (distance <= previousLimit)
                    return fare * multiplier;

                double milesInBand = Math.Min(distance, currentLimit) - previousLimit;
                fare += milesInBand * band.RatePerMile;
                previousLimit = currentLimit;
            }

            return fare * multiplier;
        }


    }
}

