using Microsoft.EntityFrameworkCore;
using TranspoManagementAPI.Models;

namespace TranspoManagementAPI.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<FareBand> FareBands { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Trip> Trips { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FareBand>().HasData(
                new FareBand { Id = 1, DistanceLimit = 1, RatePerMile = 10 },
                new FareBand { Id = 2, DistanceLimit = 6, RatePerMile = 2 },
                new FareBand { Id = 3, DistanceLimit = 16, RatePerMile = 3 },
                new FareBand { Id = 4, DistanceLimit = null, RatePerMile = 1 }
            );
        }
    }
}
