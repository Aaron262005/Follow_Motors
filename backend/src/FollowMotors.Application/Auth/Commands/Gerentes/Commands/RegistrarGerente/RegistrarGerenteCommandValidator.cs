
using FluentValidation;

namespace FollowMotors.Application.Gerentes.Commands.RegistrarGerente;

public sealed class RegistrarGerenteCommandValidator
    : AbstractValidator<RegistrarGerenteCommand> // para definir las reglas que deben cumplir los datos de RegistrarGerenteCommand.
{
    public RegistrarGerenteCommandValidator()
    {
        RuleFor(x => x.Nombre) // valida que nombre no supere los 100 caracteres permitidos 
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.Apellidos)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.Correo) // comprueva que el correo no se encuentre vacio 
            .NotEmpty()
            .EmailAddress()
            .MaximumLength(150);

        RuleFor(x => x.Password) // exige una contraña segura entre 12 y 72 carcateres 
            .NotEmpty()
            .MinimumLength(12)
            .MaximumLength(72);

        RuleFor(x => x.ConfirmarPassword)
            .Equal(x => x.Password)
            .WithMessage("Las contrasenas no coinciden.");
    }
}
