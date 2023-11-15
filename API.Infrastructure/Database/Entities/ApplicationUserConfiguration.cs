using API.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.Infrastructure.Database.Entities;

public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.Property(t => t.FirstName)
            .IsRequired()
            .HasMaxLength(64);
        builder.Property(t => t.LastName)
            .IsRequired()
            .HasMaxLength(64);
    }
}