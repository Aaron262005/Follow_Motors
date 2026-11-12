using FluentValidation;

namespace FollowMotors.Application.Auth.Commands.Login;

public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(x => x.Correo)
            .NotEmpty().WithMessage("El correo es obligatorio.")
            .EmailAddress().WithMessage("El correo no tiene un formato valido.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("La contrasena es obligatoria.");
    }
}
