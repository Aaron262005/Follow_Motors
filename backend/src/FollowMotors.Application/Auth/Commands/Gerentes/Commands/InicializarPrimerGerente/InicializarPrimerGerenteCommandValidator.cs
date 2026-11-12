
using System.Text;
using FluentValidation;

namespace FollowMotors.Application.Gerentes.Commands.InicializarPrimerGerente;

public sealed class InicializarPrimerGerenteCommandValidator
    : AbstractValidator<InicializarPrimerGerenteCommand>
{
    public InicializarPrimerGerenteCommandValidator()
    {
        // 1. Validar nombre.
        RuleFor(x => x.Nombre)
            .NotEmpty()
            .WithMessage("El nombre es obligatorio.")
            .MaximumLength(100)
            .WithMessage("El nombre no puede superar 100 caracteres.");

        // 2. Validar apellidos.
        RuleFor(x => x.Apellidos)
            .NotEmpty()
            .WithMessage("Los apellidos son obligatorios.")
            .MaximumLength(100)
            .WithMessage("Los apellidos no pueden superar 100 caracteres.");

        // 3. Validar correo electronico.
        RuleFor(x => x.Correo)
            .NotEmpty()
            .WithMessage("El correo es obligatorio.")
            .EmailAddress()
            .WithMessage("El formato del correo no es valido.")
            .MaximumLength(150)
            .WithMessage("El correo no puede superar 150 caracteres.");

        // 4. Validar contrasena.
        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("La contrasena es obligatoria.")
            .MinimumLength(12)
            .WithMessage("La contrasena debe tener al menos 12 caracteres.")
            .Must(password =>
                password is null ||
                Encoding.UTF8.GetByteCount(password) <= 72)
            .WithMessage("La contrasena no puede superar 72 bytes en UTF-8.");

        // 5. Confirmar contrasena.
        RuleFor(x => x.ConfirmarPassword)
            .NotEmpty()
            .WithMessage("Debes confirmar la contrasena.")
            .Equal(x => x.Password)
            .WithMessage("Las contrasenas no coinciden.");
    }
}
