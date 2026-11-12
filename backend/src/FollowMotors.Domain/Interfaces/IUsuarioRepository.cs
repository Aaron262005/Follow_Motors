
using FollowMotors.Domain.Entities;

namespace FollowMotors.Domain.Interfaces;

public interface IUsuarioRepository
    : IGenericRepository<Usuario>
{
    Task<Usuario?> ObtenerPorCorreoAsync(
        string correo,
        CancellationToken cancellationToken = default);

    Task<bool> ExisteCorreoAsync(
        string correo,
        CancellationToken cancellationToken = default);

    Task<string> GenerarSiguienteNumeroClienteAsync(
        CancellationToken cancellationToken = default);

    Task<bool> ExisteGerenteAsync(
        CancellationToken cancellationToken = default);
}
