using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FollowMotors.Domain.Entities;

namespace FollowMotors.Infrastructure.Persistence.Configurations;

public class RegistroServicioConfiguration : IEntityTypeConfiguration<RegistroServicio>
{
    public void Configure(EntityTypeBuilder<RegistroServicio> builder)
    {
        builder.ToTable("RegistrosServicio");
        builder.HasKey(r => r.Id);
        builder.Property(r => r.NumeroServicio).HasConversion<string>().HasMaxLength(10);
    }
}
