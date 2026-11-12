
namespace FollowMotors.Domain.Interfaces;


// Agrupa los repositorios del sistema y permite
// guardar los cambios mediante una unidad de trabajo.

public interface IUnitOfWork
{
    // Repositorio de usuarios.
    IUsuarioRepository Usuarios { get; }

    // Repositorio de motocicletas.
    IUnidadRepository Unidades { get; }

    // Repositorio de auditoria de gerentes.
    IGenericRepository<Domain.Entities.AuditoriaGerente>
        AuditoriaGerentes { get; }

    // Repositorio de citas.
    IGenericRepository<Domain.Entities.Cita>
        Citas { get; }

    // Repositorio de garantias.
    IGenericRepository<Domain.Entities.Garantia>
        Garantias { get; }

    // Repositorio de servicios realizados.
    IGenericRepository<Domain.Entities.RegistroServicio>
        RegistrosServicio { get; }

    // Confirma los cambios pendientes.
    Task<int> GuardarCambiosAsync(
        CancellationToken cancellationToken = default);
}
