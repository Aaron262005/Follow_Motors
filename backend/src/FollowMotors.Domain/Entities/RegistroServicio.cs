using FollowMotors.Domain.Common;
using FollowMotors.Domain.Enums;

namespace FollowMotors.Domain.Entities;

/// <summary>
/// Historial de cada servicio realizado a una unidad (tablas "Primer/Segundo/Tercer
/// servicio" del gerente). El kilometraje aqui es historico; no mueve la fecha del
/// siguiente mantenimiento (las motos se controlan por tiempo, ver Unidad).
/// </summary>
public class RegistroServicio : BaseEntity
{
    public Guid UnidadId { get; set; }
    public Unidad Unidad { get; set; } = null!;

    public NumeroServicio NumeroServicio { get; set; }
    public DateOnly FechaServicio { get; set; }
    public int? Kilometraje { get; set; }
    public int? Horas { get; set; }
    public string? Notas { get; set; }
}
