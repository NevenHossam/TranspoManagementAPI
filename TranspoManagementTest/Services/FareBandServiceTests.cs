
using Moq;
using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using TranspoManagementAPI.Services.Implementations;
using TranspoManagementAPI.Repositories.Interfaces;
using TranspoManagementAPI.DTO;
using TranspoManagementAPI.Models;

namespace TranspoManagementAPI.Tests.Services
{
    public class FareBandServiceTests
    {
        private readonly Mock<IFareBandRepository> _mockRepo;
        private readonly Mock<IMapper> _mockMapper;
        private readonly IMemoryCache _memoryCache;
        private readonly FareBandService _service;

        public FareBandServiceTests()
        {
            _mockRepo = new Mock<IFareBandRepository>();
            _mockMapper = new Mock<IMapper>();
            _memoryCache = new MemoryCache(new MemoryCacheOptions());
            _service = new FareBandService(_mockRepo.Object, _mockMapper.Object, _memoryCache);
        }

        [Fact]
        public async Task GetAllOrderedAsync_ShouldReturnFromCache_WhenAvailable()
        {
            var cachedData = new List<FareBandResponseDto> { new FareBandResponseDto { DistanceLimit = 10, RatePerMile = 2.5 } };
            _memoryCache.Set("FareBands", cachedData);

            var result = await _service.GetAllOrderedAsync();

            Assert.Single(result);
            Assert.Equal(10, result.First().DistanceLimit);
        }

        [Fact]
        public async Task GetAllOrderedAsync_ShouldFetchAndCache_WhenNotCached()
        {
            var bands = new List<FareBand> { new FareBand { DistanceLimit = 15, RatePerMile = 3.0 } };
            var dtos = new List<FareBandResponseDto> { new FareBandResponseDto { DistanceLimit = 15, RatePerMile = 3.0 } };

            _mockRepo.Setup(r => r.GetAllOrderedAsync()).ReturnsAsync(bands);
            _mockMapper.Setup(m => m.Map<IEnumerable<FareBandResponseDto>>(bands)).Returns(dtos);

            var result = await _service.GetAllOrderedAsync();

            Assert.Single(result);
            Assert.Equal(15, result.First().DistanceLimit);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnMapped_WhenExists()
        {
            var band = new FareBand { Id = 1 };
            var dto = new FareBandResponseDto { Id = 1 };

            _mockRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(band);
            _mockMapper.Setup(m => m.Map<FareBandResponseDto>(band)).Returns(dto);

            var result = await _service.GetByIdAsync(1);

            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnNull_WhenNotExists()
        {
            _mockRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync((FareBand?)null);

            var result = await _service.GetByIdAsync(1);

            Assert.Null(result);
        }

        [Fact]
        public async Task CreateAsync_ShouldAddAndReturnMapped()
        {
            var request = new FareBandRequestDto { DistanceLimit = 20, RatePerMile = 4 };
            var band = new FareBand { DistanceLimit = 20, RatePerMile = 4 };
            var dto = new FareBandResponseDto { DistanceLimit = 20, RatePerMile = 4 };

            _mockMapper.Setup(m => m.Map<FareBand>(request)).Returns(band);
            _mockMapper.Setup(m => m.Map<FareBandResponseDto>(band)).Returns(dto);

            var result = await _service.CreateAsync(request);

            _mockRepo.Verify(r => r.AddAsync(band), Times.Once);
            _mockRepo.Verify(r => r.SaveChangesAsync(), Times.Once);
            Assert.Equal(20, result.DistanceLimit);
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdate_WhenExists()
        {
            var band = new FareBand { Id = 1 };
            var request = new FareBandRequestDto { DistanceLimit = 25, RatePerMile = 5 };

            _mockRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(band);

            var result = await _service.UpdateAsync(1, request);

            Assert.True(result);
            _mockRepo.Verify(r => r.Update(band), Times.Once);
            _mockRepo.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_ShouldReturnFalse_WhenNotExists()
        {
            _mockRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync((FareBand?)null);

            var result = await _service.UpdateAsync(1, new FareBandRequestDto());

            Assert.False(result);
        }

        [Fact]
        public async Task DeleteAsync_ShouldDelete_WhenExists()
        {
            var band = new FareBand { Id = 1 };

            _mockRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(band);

            var result = await _service.DeleteAsync(1);

            Assert.True(result);
            _mockRepo.Verify(r => r.Delete(band), Times.Once);
            _mockRepo.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ShouldReturnFalse_WhenNotExists()
        {
            _mockRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync((FareBand?)null);

            var result = await _service.DeleteAsync(1);

            Assert.False(result);
        }
    }
}
