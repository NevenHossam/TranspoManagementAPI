using TranspoManagementAPI.Data;
using TranspoManagementAPI.Models;
using TranspoManagementAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace TranspoManagementAPI.Repositories.Implementations
{
    public class FareBandRepository : IFareBandRepository
    {
        private readonly AppDbContext _context;
        public FareBandRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<FareBand>> GetAllOrderedAsync()
        {
            return await _context.FareBands
                .OrderBy(b => b.DistanceLimit ?? double.MaxValue)
                .ToListAsync();
        }

        public async Task<FareBand?> GetByIdAsync(int id)
        {
            return await _context.FareBands.FindAsync(id);
        }

        public async Task AddAsync(FareBand band)
        {
            await _context.FareBands.AddAsync(band);
        }

        public void Update(FareBand band)
        {
            _context.FareBands.Update(band);
        }

        public void Delete(FareBand band)
        {
            _context.FareBands.Remove(band);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
