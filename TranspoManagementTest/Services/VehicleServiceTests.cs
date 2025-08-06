
using Moq;
using AutoMapper;
using TranspoManagementAPI.Services;
using TranspoManagementAPI.Repositories.Interfaces;
using TranspoManagementAPI.Models;
using TranspoManagementAPI.DTO;

namespace TranspoManagementAPI.Tests.Services
{
    public class VehicleServiceTests
    {
        private readonly Mock<IVehicleRepository> _vehicleRepoMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly VehicleService _service;

        public VehicleServiceTests()
        {
            _vehicleRepoMock = new Mock<IVehicleRepository>();
            _mapperMock = new Mock<IMapper>();
            _service = new VehicleService(_vehicleRepoMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsMappedVehicles()
        {
            var vehicles = new List<Vehicle> { new Vehicle(), new Vehicle() };
            var vehicleDtos = new List<VehicleResponseDto> { new VehicleResponseDto(), new VehicleResponseDto() };

            _vehicleRepoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(vehicles);
            _mapperMock.Setup(m => m.Map<IEnumerable<VehicleResponseDto>>(vehicles)).Returns(vehicleDtos);

            var result = await _service.GetAllAsync();

            Assert.Equal(vehicleDtos.Count, result.Count());
        }

        [Fact]
        public async Task GetByIdAsync_ExistingId_ReturnsDto()
        {
            var vehicle = new Vehicle();
            var vehicleDto = new VehicleResponseDto();

            _vehicleRepoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(vehicle);
            _mapperMock.Setup(m => m.Map<VehicleResponseDto>(vehicle)).Returns(vehicleDto);

            var result = await _service.GetByIdAsync(1);

            Assert.Equal(vehicleDto, result);
        }

        [Fact]
        public async Task GetByIdAsync_NonExistingId_ReturnsNull()
        {
            _vehicleRepoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync((Vehicle?)null);

            var result = await _service.GetByIdAsync(1);

            Assert.Null(result);
        }

        [Fact]
        public async Task CreateAsync_ValidRequest_ReturnsCreatedDto()
        {
            var request = new VehicleRequestDto();
            var vehicle = new Vehicle();
            var vehicleDto = new VehicleResponseDto();

            _mapperMock.Setup(m => m.Map<Vehicle>(request)).Returns(vehicle);
            _vehicleRepoMock.Setup(r => r.AddAsync(vehicle)).Returns(Task.CompletedTask);
            _vehicleRepoMock.Setup(r => r.SaveChangesAsync()).Returns(Task.CompletedTask);
            _mapperMock.Setup(m => m.Map<VehicleResponseDto>(vehicle)).Returns(vehicleDto);

            var result = await _service.CreateAsync(request);

            Assert.Equal(vehicleDto, result);
        }

        [Fact]
        public async Task UpdateAsync_ExistingId_UpdatesAndReturnsTrue()
        {
            var vehicle = new Vehicle();
            var request = new VehicleRequestDto { Name = "Updated", FareMultiplier = 2.5 };

            _vehicleRepoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(vehicle);
            _vehicleRepoMock.Setup(r => r.SaveChangesAsync()).Returns(Task.CompletedTask);

            var result = await _service.UpdateAsync(1, request);

            Assert.True(result);
            Assert.Equal("Updated", vehicle.Name);
            Assert.Equal(2.5, vehicle.FareMultiplier);
        }

        [Fact]
        public async Task UpdateAsync_NonExistingId_ReturnsFalse()
        {
            var request = new VehicleRequestDto();

            _vehicleRepoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync((Vehicle?)null);

            var result = await _service.UpdateAsync(1, request);

            Assert.False(result);
        }

        [Fact]
        public async Task DeleteAsync_ExistingId_DeletesAndReturnsTrue()
        {
            var vehicle = new Vehicle();

            _vehicleRepoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(vehicle);
            _vehicleRepoMock.Setup(r => r.SaveChangesAsync()).Returns(Task.CompletedTask);

            var result = await _service.DeleteAsync(1);

            Assert.True(result);
        }

        [Fact]
        public async Task DeleteAsync_NonExistingId_ReturnsFalse()
        {
            _vehicleRepoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync((Vehicle?)null);

            var result = await _service.DeleteAsync(1);

            Assert.False(result);
        }
    }
}
