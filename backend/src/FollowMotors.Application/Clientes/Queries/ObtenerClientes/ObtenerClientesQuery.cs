using MediatR;
using FollowMotors.Application.Clientes.DTOs;

namespace FollowMotors.Application.Clientes.Queries.ObtenerClientes;

/// <summary>Lista de clientes con busqueda (gerente/clientes-cuentas.html, buscarGestion).</summary>
public record ObtenerClientesQuery(string? TextoBusqueda) : IRequest<List<ClienteDto>>;
