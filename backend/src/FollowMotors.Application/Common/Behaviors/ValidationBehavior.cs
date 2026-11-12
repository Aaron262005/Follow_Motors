using FluentValidation;
using MediatR;
using FollowMotors.Application.Common.Exceptions;
using ValidationException = FollowMotors.Application.Common.Exceptions.ValidationException;

namespace FollowMotors.Application.Common.Behaviors;

/// <summary>
/// Pipeline behavior de MediatR: se ejecuta para TODO Command/Query antes de
/// llegar a su Handler. Si hay un IValidator&lt;TRequest&gt; registrado para el
/// request, lo corre y, si falla, corta la ejecucion lanzando ValidationException
/// (el Handler nunca llega a ejecutarse). Esto es justo "antes del handler
/// utiliza un validator", aplicado de forma transversal (principio Open/Closed:
/// cada nuevo Command solo necesita su propio Validator, sin tocar este archivo).
/// </summary>
public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    private readonly IEnumerable<IValidator<TRequest>> _validadores;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validadores)
    {
        _validadores = validadores;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (!_validadores.Any())
        {
            return await next();
        }

        var contexto = new ValidationContext<TRequest>(request);

        var resultados = await Task.WhenAll(
            _validadores.Select(v => v.ValidateAsync(contexto, cancellationToken)));

        var fallas = resultados
            .SelectMany(r => r.Errors)
            .Where(f => f is not null)
            .ToList();

        if (fallas.Count != 0)
        {
            throw new ValidationException(fallas);
        }

        return await next();
    }
}
