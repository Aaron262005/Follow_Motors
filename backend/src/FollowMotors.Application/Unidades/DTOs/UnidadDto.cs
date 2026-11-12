namespace FollowMotors.Application.Unidades.DTOs;

public record UnidadDto(
    Guid Id,
    Guid ClienteId,
    string NumeroSerie,
    string Categoria,
    string Modelo,
    DateOnly FechaCompra,
    string ServicioActual,
    DateOnly? FechaUltimoServicio,
    int? KilometrajeUltimoServicio,
    string? ImagenUrl);
