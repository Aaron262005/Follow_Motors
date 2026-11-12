using Microsoft.EntityFrameworkCore;
using FollowMotors.Domain.Interfaces;

namespace FollowMotors.Infrastructure.Persistence.Repositories;

/// <summary>Implementacion generica reutilizada por todos los repositorios especificos.</summary>
public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    protected readonly ApplicationDbContext Contexto;
    protected readonly DbSet<T> DbSet;

    public GenericRepository(ApplicationDbContext contexto)
    {
        Contexto = contexto;
        DbSet = contexto.Set<T>();
    }

    public async Task<T?> ObtenerPorIdAsync(Guid id, CancellationToken cancellationToken = default) =>
        await DbSet.FindAsync(new object[] { id }, cancellationToken);

    public async Task<List<T>> ObtenerTodosAsync(CancellationToken cancellationToken = default) =>
        await DbSet.ToListAsync(cancellationToken);

    public async Task AgregarAsync(T entidad, CancellationToken cancellationToken = default) =>
        await DbSet.AddAsync(entidad, cancellationToken);

    public void Actualizar(T entidad) => DbSet.Update(entidad);

    public void Eliminar(T entidad) => DbSet.Remove(entidad);
}
