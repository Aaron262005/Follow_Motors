namespace FollowMotors.Domain.Common;

/// <summary>
/// Entidad base: todo lo que vive en la BD tiene un Id y fechas de auditoria.
/// Principio SOLID aplicado: SRP (unica razon de cambio: identidad/auditoria).
/// </summary>
public abstract class BaseEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
    public DateTime? FechaActualizacion { get; set; }
}
