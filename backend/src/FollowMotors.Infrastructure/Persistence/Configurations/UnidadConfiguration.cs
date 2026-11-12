using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FollowMotors.Domain.Entities;

namespace FollowMotors.Infrastructure.Persistence.Configurations;

public class UnidadConfiguration : IEntityTypeConfiguration<Unidad>
{
    public void Configure(EntityTypeBuilder<Unidad> builder)
    {
        builder.ToTable("Unidades");
        builder.HasKey(u => u.Id);

        builder.Property(u => u.NumeroSerie).IsRequired().HasMaxLength(20);
        builder.Property(u => u.Modelo).IsRequired().HasMaxLength(100);
        builder.Property(u => u.Categoria).HasConversion<string>().HasMaxLength(10);
        builder.Property(u => u.ServicioActual).HasConversion<string>().HasMaxLength(10);

        builder.HasIndex(u => u.NumeroSerie).IsUnique();

        builder.HasMany(u => u.HistorialServicios)
            .WithOne(r => r.Unidad)
            .HasForeignKey(r => r.UnidadId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(u => u.Garantias)
            .WithOne(g => g.Unidad)
            .HasForeignKey(g => g.UnidadId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
