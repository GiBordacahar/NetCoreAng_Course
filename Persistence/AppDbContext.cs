using Microsoft.EntityFrameworkCore;
using NetCoreAng.Core.Models;

namespace NetCoreAng.Persistence
{
    public class AppDbContext : DbContext
    {
        public DbSet<Make> Makes { get; set; }
        public DbSet<Model> Models {get; set;}
        public DbSet<Feature> Features { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) 
          : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder mb) 
        {
            mb.Entity<VehicleFeature>().HasKey(vf => 
            new { vf.VehicleId, vf.FeatureId });
        }
           
    }
}