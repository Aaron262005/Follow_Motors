
using MediatR;

namespace FollowMotors.Application.Gerentes.Commands.InicializarPrimerGerente;

public sealed record InicializarPrimerGerenteCommand(
    string Nombre,
    string Apellidos,
    string Correo,
    string Password,
    string ConfirmarPassword
) : IRequest<Guid>;
