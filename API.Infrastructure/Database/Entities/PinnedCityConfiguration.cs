using API.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.Infrastructure.Database.Entities;

public class PinnedCityConfiguration : IEntityTypeConfiguration<PinnedCity>
{
    public void Configure(EntityTypeBuilder<PinnedCity> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasOne(x => x.User)
            .WithMany(u => u.PinnedCities)
            .HasForeignKey(x => x.UserId);
    }
}