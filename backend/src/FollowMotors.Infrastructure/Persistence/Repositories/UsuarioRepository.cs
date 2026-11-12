
using Microsoft.EntityFrameworkCore;
using FollowMotors.Domain.Entities;
using FollowMotors.Domain.Enums;
using FollowMotors.Domain.Interfaces;

namespace FollowMotors.Infrastructure.Persistence.Repositories;

public class UsuarioRepository
    : GenericRepository<Usuario>, IUsuarioRepository
{
    public UsuarioRepository(ApplicationDbContext contexto)
        : base(contexto)
    {
    }

    // Buscar usuario por correo
    public async Task<Usuario?> ObtenerPorCorreoAsync(
        string correo,
        CancellationToken cancellationToken = default)
    {
        return await DbSet
            .AsNoTracking()
            .FirstOrDefaultAsync(
                u => u.Correo == correo.Trim().ToLowerInvariant(),
                cancellationToken);
    }

    // Verificar si ya existe el correo
    public async Task<bool> ExisteCorreoAsync(
        string correo,
        CancellationToken cancellationToken = default)
    {
        return await DbSet
            .AnyAsync(
                u => u.Correo == correo.Trim().ToLowerInvariant(),
                cancellationToken);
    }

    // Generar numero consecutivo del cliente
    public async Task<string> GenerarSiguienteNumeroClienteAsync(
        CancellationToken cancellationToken = default)
    {
        var numeros = await DbSet
            .Where(u => u.NumeroCliente != null)
            .Select(u => u.NumeroCliente!)
            .ToListAsync(cancellationToken);

        var maximo = numeros
            .Select(n => int.TryParse(
                n.Replace("FM-", ""), out var v) ? v : 0)
            .DefaultIfEmpty(0)
            .Max();

        return $"FM-{(maximo + 1):D4}";
    }

    // Comprobar si existe un gerente registrado
    public async Task<bool> ExisteGerenteAsync(
        CancellationToken cancellationToken = default)
    {
        return await DbSet
            .AsNoTracking()
            .AnyAsync(
                u => u.Rol == RolUsuario.Gerente,
                cancellationToken);
    }
}
