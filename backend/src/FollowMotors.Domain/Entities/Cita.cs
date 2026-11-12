using FollowMotors.Domain.Common;
using FollowMotors.Domain.Enums;

namespace FollowMotors.Domain.Entities;

/// <summary>
/// Cita de la agenda del taller (gerente/agenda-servicios.html).
/// </summary>
public class Cita : BaseEntity
{
    public Guid UnidadId { get; set; }
    public Unidad Unidad { get; set; } = null!;

    public DateOnly Fecha { get; set; }
    public TimeOnly Hora { get; set; }

    public string Motivo { get; set; } = string.Empty;
    public string? TipoServicio { get; set; }
    public PrioridadCita Prioridad { get; set; } = PrioridadCita.Normal;
    public string? Asesor { get; set; }
    public string? Notas { get; set; }

    public EstadoCita Estado { get; set; } = EstadoCita.Pendiente;
}
