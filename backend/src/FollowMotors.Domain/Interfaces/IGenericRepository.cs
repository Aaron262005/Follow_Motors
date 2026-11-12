namespace FollowMotors.Domain.Interfaces;


public interface IGenericRepository<T> where T : class
{
    Task<T?> ObtenerPorIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<T>> ObtenerTodosAsync(CancellationToken cancellationToken = default);
    Task AgregarAsync(T entidad, CancellationToken cancellationToken = default);
    void Actualizar(T entidad);
    void Eliminar(T entidad);
}
