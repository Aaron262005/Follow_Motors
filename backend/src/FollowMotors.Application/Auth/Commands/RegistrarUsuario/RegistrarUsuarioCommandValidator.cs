using FluentValidation;

namespace FollowMotors.Application.Auth.Commands.RegistrarUsuario;

public class RegistrarUsuarioCommandValidator : AbstractValidator<RegistrarUsuarioCommand>
{
    public RegistrarUsuarioCommandValidator()
    {
        RuleFor(x => x.Nombre).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Apellidos).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Correo).NotEmpty().EmailAddress();
        RuleFor(x => x.Password).NotEmpty().MinimumLength(6)
            .WithMessage("La contrasena debe tener al menos 6 caracteres.");
        RuleFor(x => x.ConfirmarPassword)
            .Equal(x => x.Password).WithMessage("Las contrasenas no coinciden.");
    }
}
