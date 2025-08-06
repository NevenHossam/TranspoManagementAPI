using TranspoManagementAPI.Models;
using TranspoManagementAPI.DTO;

namespace TranspoManagementAPI.Services.Interfaces
{
    public interface IVehicleService : IService<VehicleResponseDto, Vehicle, VehicleRequestDto>
    {
        //for open closed principle
    }
}
