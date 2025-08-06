using TranspoManagementAPI.DTO;
using TranspoManagementAPI.Repositories.Interfaces;
using TranspoManagementAPI.Services.Interfaces;
using TranspoManagementAPI.Models;

namespace TranspoManagementAPI.Services
{
    public class TripService : ITripService
    {
        private readonly ITripRepository _tripRepository;
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IFareCalcService _fareCalculator;

        public TripService(ITripRepository tripRepository, IVehicleRepository vehicleRepository,
            IFareCalcService fareCalculator)
        {
            _tripRepository = tripRepository;
            _vehicleRepository = vehicleRepository;
            _fareCalculator = fareCalculator;
        }

        public async Task<IEnumerable<TripResponseDto>> GetAllAsync()
        {
            var trips = await _tripRepository.GetAllWithVehiclesAsync();
            return trips.Select(MapToDto);
        }

        public async Task<TripResponseDto?> GetByIdAsync(int id)
        {
            var trip = await _tripRepository.GetWithVehicleByIdAsync(id);
            if (trip == null) return null;
            return MapToDto(trip);
        }

        public async Task<TripResponseDto> CreateAsync(TripRequestDto request)
        {
            var vehicle = await _vehicleRepository.GetByIdAsync(request.VehicleId);
            if (vehicle == null)
                throw new Exception("Vehicle not found");
            var fare = await _fareCalculator.CalculateFare(request.Distance, vehicle.FareMultiplier);
            var trip = new Trip
            {
                Distance = request.Distance,
                VehicleId = vehicle.Id,
                TripTotalFare = fare,
                TripDate = request.TripDate,
                Vehicle = vehicle
            };
            await _tripRepository.AddAsync(trip);
            await _tripRepository.SaveChangesAsync();
            return MapToDto(trip);
        }

        public async Task<bool> UpdateAsync(int id, TripRequestDto request)
        {
            var trip = await _tripRepository.GetByIdAsync(id);
            if (trip == null)
                return false;
            var vehicle = await _vehicleRepository.GetByIdAsync(request.VehicleId);
            if (vehicle == null)
                return false;
            trip.Distance = request.Distance;
            trip.VehicleId = request.VehicleId;
            trip.TripTotalFare = await _fareCalculator.CalculateFare(request.Distance, vehicle.FareMultiplier);
            trip.TripDate = request.TripDate;
            _tripRepository.Update(trip);
            await _tripRepository.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var trip = await _tripRepository.GetByIdAsync(id);
            if (trip == null)
                return false;
            _tripRepository.Delete(trip);
            await _tripRepository.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<TripResponseDto>> GetTripsByVehicleAsync(int vehicleId)
        {
            var trips = await _tripRepository.GetAllWithVehiclesAsync();
            return trips.Where(t => t.VehicleId == vehicleId).Select(MapToDto);
        }

        public async Task<TripResponseDto?> GetTripByVehicleAsync(int vehicleId, int tripId)
        {
            var trip = await _tripRepository.GetWithVehicleByIdAsync(tripId);
            if (trip == null || trip.VehicleId != vehicleId)
                return null;
            return MapToDto(trip);
        }

        private TripResponseDto MapToDto(Trip trip)
        {
            return new TripResponseDto
            {
                Id = trip.Id,
                Distance = trip.Distance,
                TripTotalFare = trip.TripTotalFare,
                TripDate = trip.TripDate,
                VehicleId = trip.VehicleId,
                VehicleName = trip.Vehicle?.Name ?? string.Empty
            };
        }
    }
}
