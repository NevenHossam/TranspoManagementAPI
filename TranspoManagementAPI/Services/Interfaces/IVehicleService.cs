using TranspoManagementAPI.Models;
using TranspoManagementAPI.DTO;
using TranspoManagementAPI.Services.Interfaces;

namespace TranspoManagementAPI.Services.Interfaces
{
    public interface IVehicleService : IService<VehicleResponseDto, Vehicle, VehicleRequest>
    {
    }
}
