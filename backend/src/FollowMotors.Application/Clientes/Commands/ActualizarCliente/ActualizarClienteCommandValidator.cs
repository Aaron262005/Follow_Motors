using FluentValidation;

namespace FollowMotors.Application.Clientes.Commands.ActualizarCliente;

public class ActualizarClienteCommandValidator : AbstractValidator<ActualizarClienteCommand>
{
    public ActualizarClienteCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.Nombre).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Apellidos).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Correo).NotEmpty().EmailAddress();
        RuleFor(x => x.Telefono).NotEmpty().Matches(@"^\d{10}$");
    }
}
