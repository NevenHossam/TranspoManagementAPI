using TranspoManagementAPI.Models;
using TranspoManagementAPI.DTO;
using TranspoManagementAPI.Services.Interfaces;

namespace TranspoManagementAPI.Services.Interfaces
{
    public interface ITripService : IService<TripResponseDto, Trip, TripRequest>
    {
        Task<IEnumerable<TripResponseDto>> GetTripsByVehicleAsync(int vehicleId);
        Task<TripResponseDto?> GetTripByVehicleAsync(int vehicleId, int tripId);
    }
}
