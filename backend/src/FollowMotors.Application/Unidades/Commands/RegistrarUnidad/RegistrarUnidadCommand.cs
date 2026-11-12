using MediatR;
using FollowMotors.Application.Unidades.DTOs;

namespace FollowMotors.Application.Unidades.Commands.RegistrarUnidad;

/// <summary>
/// Espejo de gerente/registrar-unidad.html: registra (o reutiliza) un cliente
/// y da de alta su unidad en un solo flujo.
/// </summary>
public record RegistrarUnidadCommand(
    Guid ClienteId,
    string NumeroSerie,
    string Categoria,
    string Modelo,
    DateOnly FechaCompra,
    DateOnly? FechaUltimoServicio,
    int? KilometrajeUltimoServicio,
    string? ImagenUrl) : IRequest<UnidadDto>;
