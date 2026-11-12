using FluentValidation;

namespace FollowMotors.Application.Clientes.Commands.RegistrarCliente;

public class RegistrarClienteCommandValidator : AbstractValidator<RegistrarClienteCommand>
{
    public RegistrarClienteCommandValidator()
    {
        RuleFor(x => x.Nombre).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Apellidos).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Correo).NotEmpty().EmailAddress();
        RuleFor(x => x.Telefono)
            .NotEmpty()
            .Matches(@"^\d{10}$").WithMessage("El telefono debe tener 10 digitos, ej. 4421234567.");
        RuleFor(x => x.Password)
            .MinimumLength(6).When(x => !string.IsNullOrEmpty(x.Password))
            .WithMessage("La contrasena debe tener al menos 6 caracteres.");
    }
}
