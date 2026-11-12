using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FollowMotors.Domain.Entities;

namespace FollowMotors.Infrastructure.Persistence.Configurations;

public class GarantiaConfiguration : IEntityTypeConfiguration<Garantia>
{
    public void Configure(EntityTypeBuilder<Garantia> builder)
    {
        builder.ToTable("Garantias");
        builder.HasKey(g => g.Id);
        builder.Property(g => g.Descripcion).IsRequired().HasMaxLength(300);
    }
}
