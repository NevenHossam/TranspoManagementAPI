using Moq;
using TranspoManagementAPI.Services;
using TranspoManagementAPI.Repositories.Interfaces;
using TranspoManagementAPI.Models;
using TranspoManagementAPI.DTO;
using TranspoManagementAPI.Services.Interfaces;
using AutoMapper;

namespace TranspoManagementAPI.Tests.Services
{
    public class TripServiceTests
    {
        private readonly Mock<ITripRepository> _tripRepoMock = new();
        private readonly Mock<IVehicleRepository> _vehicleRepoMock = new();
        private readonly Mock<IFareCalcService> _fareCalcServiceMock = new();
        private readonly IMapper _mapper;

        private readonly TripService _tripService;

        public TripServiceTests()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TripRequestDto, Trip>();
                cfg.CreateMap<Trip, TripResponseDto>();
            });
            _mapper = config.CreateMapper();

            _tripService = new TripService(
                _tripRepoMock.Object,
                _vehicleRepoMock.Object,
                _fareCalcServiceMock.Object
            );
        }

        [Fact]
        public async Task GetAllAsync_ReturnsTripDtos()
        {
            // Arrange
            var trips = new List<Trip>
            {
                new Trip
                {
                    Id = 1,
                    Distance = 10,
                    TripTotalFare = 100,
                    TripDate = DateTime.Now,
                    VehicleId = 1,
                    Vehicle = new Vehicle { Name = "tesla", Id = 1, FareMultiplier = 2 }
                }
            };

            _tripRepoMock.Setup(repo => repo.GetAllWithVehiclesAsync()).ReturnsAsync(trips);

            // Act
            var result = await _tripService.GetAllAsync();

            // Assert
            Assert.Single(result);
            Assert.Equal(1, result.First().Id);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsTripDto_WhenTripExists()
        {
            var trip = new Trip { Id = 1, VehicleId = 1, Vehicle = new Vehicle { Name = "Car" } };
            _tripRepoMock.Setup(repo => repo.GetWithVehicleByIdAsync(1)).ReturnsAsync(trip);

            var result = await _tripService.GetByIdAsync(1);

            Assert.NotNull(result);
            Assert.Equal(1, result?.Id);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsNull_WhenTripNotFound()
        {
            _tripRepoMock.Setup(repo => repo.GetWithVehicleByIdAsync(99)).ReturnsAsync((Trip?)null);

            var result = await _tripService.GetByIdAsync(99);

            Assert.Null(result);
        }

        [Fact]
        public async Task CreateAsync_ThrowsException_WhenVehicleNotFound()
        {
            var request = new TripRequestDto { VehicleId = 999 };
            _vehicleRepoMock.Setup(r => r.GetByIdAsync(request.VehicleId)).ReturnsAsync((Vehicle?)null);

            await Assert.ThrowsAsync<Exception>(() => _tripService.CreateAsync(request));
        }

        [Fact]
        public async Task CreateAsync_CreatesTripSuccessfully()
        {
            var request = new TripRequestDto
            {
                VehicleId = 1,
                Distance = 20,
                TripDate = DateTime.Now
            };
            var vehicle = new Vehicle { Id = 1, FareMultiplier = 2.0, Name = "Van" };

            _vehicleRepoMock.Setup(r => r.GetByIdAsync(request.VehicleId)).ReturnsAsync(vehicle);
            _fareCalcServiceMock.Setup(f => f.CalculateFare(request.Distance, vehicle.FareMultiplier)).ReturnsAsync(40);
            _tripRepoMock.Setup(r => r.AddAsync(It.IsAny<Trip>())).Returns(Task.CompletedTask);
            _tripRepoMock.Setup(r => r.SaveChangesAsync()).Returns(Task.CompletedTask);

            var result = await _tripService.CreateAsync(request);

            Assert.Equal(40, result.TripTotalFare);
            Assert.Equal(vehicle.Id, result.VehicleId);
        }


        [Fact]
        public async Task UpdateAsync_ReturnsFalse_WhenTripNotFound()
        {
            _tripRepoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync((Trip?)null);

            var result = await _tripService.UpdateAsync(1, new TripRequestDto());

            Assert.False(result);
        }

        [Fact]
        public async Task UpdateAsync_ReturnsFalse_WhenVehicleNotFound()
        {
            var trip = new Trip { Id = 1, VehicleId = 1 };
            _tripRepoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(trip);
            _vehicleRepoMock.Setup(r => r.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Vehicle?)null);

            var result = await _tripService.UpdateAsync(1, new TripRequestDto());

            Assert.False(result);
        }

        [Fact]
        public async Task DeleteAsync_ReturnsFalse_WhenTripNotFound()
        {
            _tripRepoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync((Trip?)null);

            var result = await _tripService.DeleteAsync(1);

            Assert.False(result);
        }

        [Fact]
        public async Task DeleteAsync_DeletesTripSuccessfully()
        {
            var trip = new Trip { Id = 1 };
            _tripRepoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(trip);
            _tripRepoMock.Setup(r => r.Delete(trip));
            _tripRepoMock.Setup(r => r.SaveChangesAsync()).Returns(Task.CompletedTask);

            var result = await _tripService.DeleteAsync(1);

            Assert.True(result);
        }

        [Fact]
        public async Task GetTripsByVehicleAsync_FiltersCorrectly()
        {
            var trips = new List<Trip>
            {
                new Trip { Id = 1, VehicleId = 1, Vehicle = new Vehicle { Name = "Car" } },
                new Trip { Id = 2, VehicleId = 2, Vehicle = new Vehicle { Name = "Van" } }
            };
            _tripRepoMock.Setup(repo => repo.GetAllWithVehiclesAsync()).ReturnsAsync(trips);

            var result = await _tripService.GetTripsByVehicleAsync(1);

            Assert.Single(result);
            Assert.Equal(1, result.First().VehicleId);
        }

        [Fact]
        public async Task GetTripByVehicleAsync_ReturnsCorrectTrip()
        {
            var trip = new Trip { Id = 1, VehicleId = 1, Vehicle = new Vehicle { Name = "Car" } };
            _tripRepoMock.Setup(repo => repo.GetWithVehicleByIdAsync(1)).ReturnsAsync(trip);

            var result = await _tripService.GetTripByVehicleAsync(1, 1);

            Assert.NotNull(result);
            Assert.Equal(1, result?.VehicleId);
        }

        [Fact]
        public async Task GetTripByVehicleAsync_ReturnsNull_IfVehicleMismatch()
        {
            var trip = new Trip { Id = 1, VehicleId = 2 };
            _tripRepoMock.Setup(repo => repo.GetWithVehicleByIdAsync(1)).ReturnsAsync(trip);

            var result = await _tripService.GetTripByVehicleAsync(1, 1);

            Assert.Null(result);
        }
    }
}
