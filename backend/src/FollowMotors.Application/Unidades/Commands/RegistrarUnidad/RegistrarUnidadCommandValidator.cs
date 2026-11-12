using FluentValidation;
using FollowMotors.Domain.Enums;

namespace FollowMotors.Application.Unidades.Commands.RegistrarUnidad;

public class RegistrarUnidadCommandValidator : AbstractValidator<RegistrarUnidadCommand>
{
    public RegistrarUnidadCommandValidator()
    {
        RuleFor(x => x.ClienteId).NotEmpty();

        RuleFor(x => x.NumeroSerie)
            .NotEmpty()
            .Length(17).WithMessage("El numero de serie (VIN) debe tener 17 caracteres.");

        RuleFor(x => x.Categoria)
            .NotEmpty()
            .Must(c => Enum.TryParse<CategoriaUnidad>(c, true, out _))
            .WithMessage("Categoria invalida. Usa Moto, Atv, Ssv o Utv.");

        RuleFor(x => x.Modelo).NotEmpty().MaximumLength(100);

        RuleFor(x => x.FechaCompra)
            .NotEmpty()
            .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.UtcNow))
            .WithMessage("La fecha de compra no puede ser futura.");

        RuleFor(x => x.KilometrajeUltimoServicio)
            .GreaterThanOrEqualTo(0)
            .When(x => x.KilometrajeUltimoServicio.HasValue);
    }
}
