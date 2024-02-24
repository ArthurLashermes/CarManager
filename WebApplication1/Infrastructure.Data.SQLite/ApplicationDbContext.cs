using Microsoft.EntityFrameworkCore;
using Server.Domain;

namespace WebApplication1
{
    public class ApplicationDbContext : DbContext
    {

		public DbSet<Brand> Brands { get; set; }
		public DbSet<CarModel> CarModels { get; set; }
		public DbSet<Vehicle> Vehicles { get; set; }
		public DbSet<Maintenance> Maintenances { get; set; }

		public ApplicationDbContext(DbContextOptions options):
        base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

			// Configure the Brand-CarModel relationship
			modelBuilder.Entity<Brand>()
				.HasMany(b => b.CarModels) // Brand has many CarModels
				.WithOne(cm => cm.Brand) // CarModel has one Brand
				.HasForeignKey(cm => cm.BrandId); // ForeignKey in CarModel pointing back to Brand

			// Configure the CarModel-Vehicle relationship
			modelBuilder.Entity<CarModel>()
				.HasMany(cm => cm.Vehicles) // CarModel has many Vehicles
				.WithOne(v => v.CarModel) // Vehicle has one CarModel
				.HasForeignKey(v => v.CarModelId); // ForeignKey in Vehicle pointing back to CarModel

			// Configure the Vehicle-Maintenance relationship
			modelBuilder.Entity<Vehicle>()
				.HasMany(v => v.Maintenances) // Vehicle has many Maintenances
				.WithOne(m => m.Vehicle) // Maintenance has one Vehicle
				.HasForeignKey(m => m.VehicleId); // ForeignKey in Maintenance pointing back to Vehicle
		}
    }
}
