namespace TestProject.Mock;
using Microsoft.EntityFrameworkCore;
using Server.Domain;
public class ApplicationDbContextMocked : DbContext
{
    public ApplicationDbContextMocked()
    { }
    public DbSet<Brand> Brands { get; set; }
    public DbSet<Car> Cars { get; set; }
    public virtual DbSet<Vehicle> Vehicles { get; set; }
    public DbSet<Maintenance> Maintenances { get; set; }

    public ApplicationDbContextMocked(DbContextOptions options) :
        base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure the Brand-Car relationship
        modelBuilder.Entity<Brand>()
            .HasMany(b => b.Car) // Brand has many Cars
            .WithOne(cm => cm.Brand) // Car has one Brand
            .HasForeignKey(cm => cm.BrandId); // ForeignKey in Car pointing back to Brand

        // Configure the Car-Vehicle relationship
        modelBuilder.Entity<Car>()
            .HasMany(cm => cm.Vehicles) // Car has many Vehicles
            .WithOne(v => v.Car) // Vehicle has one Car
            .HasForeignKey(v => v.CarId); // ForeignKey in Vehicle pointing back to Car

        // Configure the Vehicle-Maintenance relationship
        modelBuilder.Entity<Vehicle>()
            .HasMany(v => v.Maintenances) // Vehicle has many Maintenances
            .WithOne(m => m.Vehicle) // Maintenance has one Vehicle
            .HasForeignKey(m => m.VehicleId); // ForeignKey in Maintenance pointing back to Vehicle
    }
}