
using MediatR; // inportar Mediatr

namespace FollowMotors.Application.Gerentes.Commands.RegistrarGerente;
// Utilizamos un record para representar los datos de la solicitud
// los commands solo transportan la informacion 
public record RegistrarGerenteCommand( //
    string Nombre,
    string Apellidos,
    string Correo,
    string Password,
    string ConfirmarPassword
) : IRequest<Guid>;
// la contraseña se transporta al handler 