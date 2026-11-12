
using FollowMotors.Domain.Entities;
using FollowMotors.Domain.Interfaces;
using FollowMotors.Infrastructure.Persistence.Repositories;

namespace FollowMotors.Infrastructure.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _contexto; // representa el contexto de Entity Framework Core que utilizamos para interactuar con MySQL.

    public UnitOfWork(
        ApplicationDbContext contexto,
        IUsuarioRepository usuarios,
        IUnidadRepository unidades)
    {
        _contexto = contexto;

        // Repositorios existentes
        Usuarios = usuarios;
        Unidades = unidades;

        Citas = new GenericRepository<Cita>(contexto);

        Garantias = new GenericRepository<Garantia>(contexto);

        RegistrosServicio =
            new GenericRepository<RegistroServicio>(contexto);

        // Nuevo repositorio para auditoria de gerentes
        AuditoriaGerentes =
            new GenericRepository<AuditoriaGerente>(contexto);
    }

    // Repositorios existentes
    public IUsuarioRepository Usuarios { get; }

    public IUnidadRepository Unidades { get; }

    public IGenericRepository<Cita> Citas { get; }

    public IGenericRepository<Garantia> Garantias { get; }

    public IGenericRepository<RegistroServicio>
        RegistrosServicio { get; }

    // Nuevo repositorio de auditoria
    public IGenericRepository<AuditoriaGerente>
        AuditoriaGerentes { get; }

    // Guardar cambios en la base de datos
    public async Task<int> GuardarCambiosAsync(
        CancellationToken cancellationToken = default)
    {
        return await _contexto.SaveChangesAsync(
            cancellationToken);
    }
}
