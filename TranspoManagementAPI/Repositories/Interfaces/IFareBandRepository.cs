using TranspoManagementAPI.Models;

namespace TranspoManagementAPI.Repositories.Interfaces
{
    public interface IFareBandRepository: IRepository<FareBand>
    {
        Task<IEnumerable<FareBand>> GetAllOrderdByDistanceAsync();
    }
}
