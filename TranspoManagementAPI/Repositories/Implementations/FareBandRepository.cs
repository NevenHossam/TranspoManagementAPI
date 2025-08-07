using TranspoManagementAPI.Data;
using TranspoManagementAPI.Models;
using TranspoManagementAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace TranspoManagementAPI.Repositories.Implementations
{
    public class FareBandRepository : Repository<FareBand>, IFareBandRepository
    {
        public FareBandRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<FareBand>> GetAllOrderdByDistanceAsync()
        {
            return await _context.FareBands
                .OrderBy(b => b.DistanceLimit ?? double.MaxValue)
                .ToListAsync();
        }
    }
}
