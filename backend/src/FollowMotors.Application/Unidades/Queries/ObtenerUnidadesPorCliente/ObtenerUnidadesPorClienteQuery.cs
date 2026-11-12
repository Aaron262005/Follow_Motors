using MediatR;
using FollowMotors.Application.Unidades.DTOs;

namespace FollowMotors.Application.Unidades.Queries.ObtenerUnidadesPorCliente;

/// <summary>Espejo de cliente/cliente.html: "mis unidades".</summary>
public record ObtenerUnidadesPorClienteQuery(Guid ClienteId) : IRequest<List<UnidadDto>>;
