using TranspoManagementAPI.Models;

namespace TranspoManagementAPI.Repositories.Interfaces
{
    public interface ITripRepository: IRepository<Trip>
    {
        Task<IEnumerable<Trip>> GetAllWithVehiclesAsync();
        Task<Trip?> GetWithVehicleByIdAsync(int id);
    }
}

