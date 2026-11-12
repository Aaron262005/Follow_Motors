using FollowMotors.Domain.Common;

namespace FollowMotors.Domain.Entities;

/// <summary>
/// Garantia asociada a una unidad (gerente/garantias-finalizadas.html).
/// </summary>
public class Garantia : BaseEntity
{
    public Guid UnidadId { get; set; }
    public Unidad Unidad { get; set; } = null!;

    public string Descripcion { get; set; } = string.Empty;
    public DateOnly FechaInicio { get; set; }
    public DateOnly? FechaFinalizacion { get; set; }
    public bool Finalizada { get; set; }
}
