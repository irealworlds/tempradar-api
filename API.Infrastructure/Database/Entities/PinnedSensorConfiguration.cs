using API.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.Infrastructure.Database.Entities;

public class PinnedSensorConfiguration : IEntityTypeConfiguration<PinnedSensor>
{
    public void Configure(EntityTypeBuilder<PinnedSensor> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasOne(x => x.User)
            .WithMany(u => u.PinnedSensors)
            .HasForeignKey(x => x.UserId);
        builder.HasIndex(x => new { x.SensorId, x.UserId })
            .IsUnique();
    }
}