using API.Domain.Entities;
using API.Infrastructure.Database.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace API.Infrastructure.Database;

public class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<PinnedCity> PinnedCities { get; set; }
    public DbSet<PinnedSensor> PinnedSensors { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new ApplicationUserConfiguration());
        modelBuilder.ApplyConfiguration(new PinnedCityConfiguration());
        modelBuilder.ApplyConfiguration(new PinnedSensorConfiguration());
    }
}