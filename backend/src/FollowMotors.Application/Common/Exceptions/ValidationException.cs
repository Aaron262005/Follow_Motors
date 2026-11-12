using FluentValidation.Results;

namespace FollowMotors.Application.Common.Exceptions;

/// <summary>
/// La lanza ValidationBehavior cuando un Validator rechaza un Command/Query.
/// El WebApi la traduce a 400 Bad Request (ver Middleware/ExceptionHandlingMiddleware).
/// </summary>
public class ValidationException : Exception
{
    public IDictionary<string, string[]> Errores { get; }

    public ValidationException() : base("Se encontraron uno o mas errores de validacion.")
    {
        Errores = new Dictionary<string, string[]>();
    }

    public ValidationException(IEnumerable<ValidationFailure> fallas) : this()
    {
        Errores = fallas
            .GroupBy(f => f.PropertyName, f => f.ErrorMessage)
            .ToDictionary(g => g.Key, g => g.ToArray());
    }
}
