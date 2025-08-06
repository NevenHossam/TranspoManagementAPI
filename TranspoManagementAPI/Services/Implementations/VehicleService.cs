using AutoMapper;
using TranspoManagementAPI.DTO;
using TranspoManagementAPI.Repositories.Interfaces;
using TranspoManagementAPI.Services.Interfaces;
using TranspoManagementAPI.Models;

namespace TranspoManagementAPI.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IMapper _mapper;

        public VehicleService(IVehicleRepository vehicleRepository, IMapper mapper)
        {
            _vehicleRepository = vehicleRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<VehicleResponseDto>> GetAllAsync()
        {
            var vehicles = await _vehicleRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<VehicleResponseDto>>(vehicles);
        }

        public async Task<VehicleResponseDto?> GetByIdAsync(int id)
        {
            var v = await _vehicleRepository.GetByIdAsync(id);
            if (v == null) return null;
            return _mapper.Map<VehicleResponseDto>(v);
        }

        public async Task<VehicleResponseDto> CreateAsync(VehicleRequestDto request)
        {
            var vehicle = _mapper.Map<Vehicle>(request);
            await _vehicleRepository.AddAsync(vehicle);
            await _vehicleRepository.SaveChangesAsync();
            return _mapper.Map<VehicleResponseDto>(vehicle);
        }

        public async Task<bool> UpdateAsync(int id, VehicleRequestDto request)
        {
            var vehicle = await _vehicleRepository.GetByIdAsync(id);
            if (vehicle == null)
                return false;
            vehicle.Name = request.Name;
            vehicle.FareMultiplier = request.FareMultiplier;
            _vehicleRepository.Update(vehicle);
            await _vehicleRepository.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var vehicle = await _vehicleRepository.GetByIdAsync(id);
            if (vehicle == null)
                return false;
            _vehicleRepository.Delete(vehicle);
            await _vehicleRepository.SaveChangesAsync();
            return true;
        }
    }
}
