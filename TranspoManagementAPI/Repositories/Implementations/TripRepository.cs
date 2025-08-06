using Microsoft.EntityFrameworkCore;
using TranspoManagementAPI.Data;
using TranspoManagementAPI.Repositories.Interfaces;
using TranspoManagementAPI.Models;

namespace TranspoManagementAPI.Repositories
{
    public class TripRepository : Repository<Trip>, ITripRepository
    {
        public TripRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<Trip>> GetAllWithVehiclesAsync()
        {
            return await _context.Trips
                .Include(t => t.Vehicle)
                .ToListAsync();
        }

        public async Task<Trip?> GetWithVehicleByIdAsync(int id)
        {
            return await _context.Trips
                .AsNoTracking()
                .Include(t => t.Vehicle)
                .FirstOrDefaultAsync(t => t.Id == id);
        }
    }
}
