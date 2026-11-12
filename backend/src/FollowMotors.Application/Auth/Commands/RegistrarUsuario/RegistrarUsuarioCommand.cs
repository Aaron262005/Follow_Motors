using MediatR;

namespace FollowMotors.Application.Auth.Commands.RegistrarUsuario;

/// <summary>Auto-registro de un cliente nuevo (registro.html).</summary>
public record RegistrarUsuarioCommand(
    string Nombre,
    string Apellidos,
    string Correo,
    string Password,
    string ConfirmarPassword) : IRequest<Guid>;
