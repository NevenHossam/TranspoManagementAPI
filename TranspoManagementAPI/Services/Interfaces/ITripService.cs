using TranspoManagementAPI.Models;
using TranspoManagementAPI.DTO;

namespace TranspoManagementAPI.Services.Interfaces
{
    public interface ITripService : IService<TripResponseDto, Trip, TripRequestDto>
    {
        Task<IEnumerable<TripResponseDto>> GetTripsByVehicleAsync(int vehicleId);
        Task<TripResponseDto?> GetTripByVehicleAsync(int vehicleId, int tripId);
    }
}
