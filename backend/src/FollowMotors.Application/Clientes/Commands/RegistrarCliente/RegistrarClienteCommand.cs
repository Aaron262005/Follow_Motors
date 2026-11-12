using MediatR;
using FollowMotors.Application.Clientes.DTOs;

namespace FollowMotors.Application.Clientes.Commands.RegistrarCliente;

/// <summary>
/// Alta de cliente hecha por el gerente (gerente/registrar-unidad.html, parte
/// "datos del cliente"). Si el cliente ya existe, el Handler lo reutiliza en
/// vez de duplicarlo (ver RegistrarUnidadCommandHandler).
/// </summary>
public record RegistrarClienteCommand(
    string Nombre,
    string Apellidos,
    string Correo,
    string Telefono,
    string? Password) : IRequest<ClienteDto>;
