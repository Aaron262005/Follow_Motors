
using FollowMotors.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FollowMotors.Infrastructure.Persistence.Configurations;

public sealed class AuditoriaGerenteConfiguration
    : IEntityTypeConfiguration<AuditoriaGerente>
{
    public void Configure(
        EntityTypeBuilder<AuditoriaGerente> builder)
    {
        // Nombre de la tabla en MySQL.
        // La tabla se creara con un script SQL independiente.
        builder.ToTable(
            "AuditoriaGerentes",
            t => t.ExcludeFromMigrations());

        // Llave primaria.
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnType("char(36)");

        // Usuario que realizo el registro.
        builder.Property(x => x.ActorUsuarioId)
            .HasColumnType("char(36)");

        // Gerente registrado.
        builder.Property(x => x.GerenteId)
            .HasColumnType("char(36)")
            .IsRequired();

        // Tipo de operacion realizada.
        builder.Property(x => x.Accion)
            .HasMaxLength(40)
            .IsRequired();

        // Fecha y hora de la operacion.
        builder.Property(x => x.FechaUtc)
            .HasColumnType("datetime(6)")
            .IsRequired();

        // Indice para consultar auditorias
        // correspondientes a un gerente.
        builder.HasIndex(x => x.GerenteId);
    }
}
