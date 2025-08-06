using TranspoManagementAPI.DTO;

namespace TranspoManagementAPI.Services.Interfaces
{
    public interface IFareBandService
    {
        Task<IEnumerable<FareBandResponseDto>> GetAllOrderedAsync();
        Task<FareBandResponseDto?> GetByIdAsync(int id);
        Task<FareBandResponseDto> CreateAsync(FareBandRequestDto request);
        Task<bool> UpdateAsync(int id, FareBandRequestDto request);
        Task<bool> DeleteAsync(int id);
    }
}
