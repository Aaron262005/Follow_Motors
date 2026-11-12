
using Microsoft.EntityFrameworkCore;
using FollowMotors.Domain.Entities;

namespace FollowMotors.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext // acceso a la base de datos de nuestra aplicacion de Follow 
{
    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
// DbSet<T> permite consultar y administrar entidades de un tipo específico mediante Entity Framework

    // Repositorio de usuarios
    public DbSet<Usuario> Usuarios
        => Set<Usuario>();

    // auditoria de registro de gerentes
    public DbSet<AuditoriaGerente> AuditoriaGerentes
        => Set<AuditoriaGerente>();

    // Repositorio de motocicletas
    public DbSet<Unidad> Unidades
        => Set<Unidad>();

    // Repositorio de citas
    public DbSet<Cita> Citas
        => Set<Cita>();

    // Repositorio de garantias
    public DbSet<Garantia> Garantias
        => Set<Garantia>();

    // Repositorio de servicios realizados
    public DbSet<RegistroServicio> RegistrosServicio
        => Set<RegistroServicio>();

    protected override void OnModelCreating(
        ModelBuilder modelBuilder)
    {
        // Cargar las configuraciones de las entidades
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(ApplicationDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}
