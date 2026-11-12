using MediatR;

namespace FollowMotors.Application.Auth.Commands.Login;

public record LoginCommand(string Correo, string Password) : IRequest<LoginResult>;

public record LoginResult(
    string Token,
    Guid UsuarioId,
    string NombreCompleto,
    string Rol,
    string? NumeroCliente);
