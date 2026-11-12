using FollowMotors.Domain.Entities;

namespace FollowMotors.Domain.Interfaces;

public interface IUnidadRepository : IGenericRepository<Unidad>
{
    Task<Unidad?> ObtenerPorNumeroSerieAsync(string numeroSerie, CancellationToken cancellationToken = default);
    Task<bool> ExisteNumeroSerieAsync(string numeroSerie, CancellationToken cancellationToken = default);
    Task<List<Unidad>> ObtenerPorClienteIdAsync(Guid clienteId, CancellationToken cancellationToken = default);
}
