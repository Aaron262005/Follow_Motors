using Microsoft.EntityFrameworkCore;
using FollowMotors.Domain.Entities;
using FollowMotors.Domain.Interfaces;

namespace FollowMotors.Infrastructure.Persistence.Repositories;

public class UnidadRepository : GenericRepository<Unidad>, IUnidadRepository
{
    public UnidadRepository(ApplicationDbContext contexto) : base(contexto) { }

    public async Task<Unidad?> ObtenerPorNumeroSerieAsync(string numeroSerie, CancellationToken cancellationToken = default) =>
        await DbSet.FirstOrDefaultAsync(u => u.NumeroSerie == numeroSerie.Trim().ToUpper(), cancellationToken);

    public async Task<bool> ExisteNumeroSerieAsync(string numeroSerie, CancellationToken cancellationToken = default) =>
        await DbSet.AnyAsync(u => u.NumeroSerie == numeroSerie.Trim().ToUpper(), cancellationToken);

    public async Task<List<Unidad>> ObtenerPorClienteIdAsync(Guid clienteId, CancellationToken cancellationToken = default) =>
        await DbSet.Where(u => u.ClienteId == clienteId).ToListAsync(cancellationToken);
}
