using AutoMapper;
using TranspoManagementAPI.DTO;
using TranspoManagementAPI.Models;
using TranspoManagementAPI.Repositories.Interfaces;
using TranspoManagementAPI.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace TranspoManagementAPI.Services.Implementations
{
    public class FareBandService : IFareBandService
    {
        private readonly IFareBandRepository _repo;
        private readonly IMapper _mapper;
        public FareBandService(IFareBandRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<FareBandResponseDto>> GetAllOrderedAsync()
        {
            var bands = await _repo.GetAllOrderedAsync();
            return _mapper.Map<IEnumerable<FareBandResponseDto>>(bands);
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
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var band = await _repo.GetByIdAsync(id);
            if (band == null) return false;
            _repo.Delete(band);
            await _repo.SaveChangesAsync();
            return true;
        }
    }
}
