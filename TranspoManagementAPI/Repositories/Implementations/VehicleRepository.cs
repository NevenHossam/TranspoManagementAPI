using TranspoManagementAPI.Data;
using TranspoManagementAPI.Repositories.Interfaces;
using TranspoManagementAPI.Models;

namespace TranspoManagementAPI.Repositories
{
    public class VehicleRepository : Repository<Vehicle>, IVehicleRepository
    {
        public VehicleRepository(AppDbContext context) : base(context) { }
    }
}
