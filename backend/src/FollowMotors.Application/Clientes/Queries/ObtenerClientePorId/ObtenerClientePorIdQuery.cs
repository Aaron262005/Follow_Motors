using MediatR;
using FollowMotors.Application.Clientes.DTOs;

namespace FollowMotors.Application.Clientes.Queries.ObtenerClientePorId;

public record ObtenerClientePorIdQuery(Guid Id) : IRequest<ClienteDto>;
