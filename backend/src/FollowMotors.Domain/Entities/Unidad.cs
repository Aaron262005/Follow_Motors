using FollowMotors.Domain.Common;
using FollowMotors.Domain.Enums;

namespace FollowMotors.Domain.Entities;

/// <summary>
/// Moto/ATV/SSV/UTV registrada a un cliente (gerente/registrar-unidad.html).
/// </summary>
public class Unidad : BaseEntity
{
    public Guid ClienteId { get; set; }
    public Usuario Cliente { get; set; } = null!;

    /// <summary>VIN / numero de serie, ej. "LCEPEWL1XP6001234".</summary>
    public string NumeroSerie { get; set; } = string.Empty;

    public CategoriaUnidad Categoria { get; set; }
    public string Modelo { get; set; } = string.Empty;

    public DateOnly FechaCompra { get; set; }

    /// <summary>Servicio en curso (1, 2 o 3) segun las reglas de REGLAS en servicios.js.</summary>
    public NumeroServicio ServicioActual { get; set; } = NumeroServicio.Primero;

    public DateOnly? FechaUltimoServicio { get; set; }
    public int? KilometrajeUltimoServicio { get; set; }
    public int? HorasUltimoServicio { get; set; }

    public string? ImagenUrl { get; set; }

    public ICollection<RegistroServicio> HistorialServicios { get; set; } = new List<RegistroServicio>();
    public ICollection<Garantia> Garantias { get; set; } = new List<Garantia>();
}
