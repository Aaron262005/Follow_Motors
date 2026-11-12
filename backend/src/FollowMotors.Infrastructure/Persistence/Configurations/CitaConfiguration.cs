using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FollowMotors.Domain.Entities;

namespace FollowMotors.Infrastructure.Persistence.Configurations;

public class CitaConfiguration : IEntityTypeConfiguration<Cita>
{
    public void Configure(EntityTypeBuilder<Cita> builder)
    {
        builder.ToTable("Citas");
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Motivo).IsRequired().HasMaxLength(100);
        builder.Property(c => c.Prioridad).HasConversion<string>().HasMaxLength(10);
        builder.Property(c => c.Estado).HasConversion<string>().HasMaxLength(15);

        builder.HasOne(c => c.Unidad)
            .WithMany()
            .HasForeignKey(c => c.UnidadId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
