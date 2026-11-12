using MediatR;
using FollowMotors.Application.Clientes.DTOs;

namespace FollowMotors.Application.Clientes.Commands.ActualizarCliente;

/// <summary>Edicion de cliente (gerente/clientes-cuentas.html, modalCliente).</summary>
public record ActualizarClienteCommand(
    Guid Id,
    string Nombre,
    string Apellidos,
    string Telefono,
    string Correo) : IRequest<ClienteDto>;
