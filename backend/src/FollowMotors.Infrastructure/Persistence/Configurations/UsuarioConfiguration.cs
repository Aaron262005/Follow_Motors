using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FollowMotors.Domain.Entities;

namespace FollowMotors.Infrastructure.Persistence.Configurations;

public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> builder)
    {
        builder.ToTable("Usuarios");
        builder.HasKey(u => u.Id);

        builder.Property(u => u.Nombre).IsRequired().HasMaxLength(100);
        builder.Property(u => u.Apellidos).IsRequired().HasMaxLength(100);
        builder.Property(u => u.Correo).IsRequired().HasMaxLength(150);
        builder.Property(u => u.Telefono).HasMaxLength(20);
        builder.Property(u => u.PasswordHash).IsRequired();
        builder.Property(u => u.NumeroCliente).HasMaxLength(20);
        builder.Property(u => u.Rol).HasConversion<string>().HasMaxLength(20);

        builder.HasIndex(u => u.Correo).IsUnique();
        builder.HasIndex(u => u.NumeroCliente).IsUnique();

        builder.HasMany(u => u.Unidades)
            .WithOne(un => un.Cliente)
            .HasForeignKey(un => un.ClienteId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
