using TranspoManagementAPI.Models;

namespace TranspoManagementAPI.Repositories.Interfaces
{
    public interface IFareBandRepository
    {
        Task<IEnumerable<FareBand>> GetAllOrderedAsync();
        Task<FareBand?> GetByIdAsync(int id);
        Task AddAsync(FareBand band);
        void Update(FareBand band);
        void Delete(FareBand band);
        Task SaveChangesAsync();
    }
}
