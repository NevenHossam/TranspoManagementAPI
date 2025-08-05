using AutoMapper;
using TranspoManagementAPI.DTO;
using TranspoManagementAPI.Models;
using TranspoManagementAPI.Repositories.Interfaces;
using TranspoManagementAPI.Services.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace TranspoManagementAPI.Services.Implementations
{
    public class FareBandService : IFareBandService
    {
        private readonly IFareBandRepository _repo;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _cache;
        private const string FareBandsCacheKey = "FareBands";

        public FareBandService(IFareBandRepository repo, IMapper mapper, IMemoryCache cache)
        {
            _repo = repo;
            _mapper = mapper;
            _cache = cache;
        }

        public async Task<IEnumerable<FareBandResponseDto>> GetAllOrderedAsync()
        {
            if (!_cache.TryGetValue(FareBandsCacheKey, out IEnumerable<FareBandResponseDto>? cachedBands))
            {
                var bands = await _repo.GetAllOrderedAsync();
                cachedBands = _mapper.Map<IEnumerable<FareBandResponseDto>>(bands);
                _cache.Set(FareBandsCacheKey, cachedBands, TimeSpan.FromMinutes(10));
            }
            return cachedBands ?? Enumerable.Empty<FareBandResponseDto>();
        }

        public async Task<FareBandResponseDto?> GetByIdAsync(int id)
        {
            var band = await _repo.GetByIdAsync(id);
            if (band == null) return null;
            return _mapper.Map<FareBandResponseDto>(band);
        }

        public async Task<FareBandResponseDto> CreateAsync(FareBandRequestDto request)
        {
            var band = _mapper.Map<FareBand>(request);
            await _repo.AddAsync(band);
            await _repo.SaveChangesAsync();
            _cache.Remove(FareBandsCacheKey); 
            return _mapper.Map<FareBandResponseDto>(band);
        }

        public async Task<bool> UpdateAsync(int id, FareBandRequestDto request)
        {
            var band = await _repo.GetByIdAsync(id);
            if (band == null) return false;
            band.DistanceLimit = request.DistanceLimit;
            band.RatePerMile = request.RatePerMile;
            _repo.Update(band);
            await _repo.SaveChangesAsync();
            _cache.Remove(FareBandsCacheKey); 
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var band = await _repo.GetByIdAsync(id);
            if (band == null) return false;
            _repo.Delete(band);
            await _repo.SaveChangesAsync();
            _cache.Remove(FareBandsCacheKey);
            return true;
        }
    }
}
