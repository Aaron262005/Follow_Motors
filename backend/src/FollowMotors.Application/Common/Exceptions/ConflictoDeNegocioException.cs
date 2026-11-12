namespace FollowMotors.Application.Common.Exceptions;

/// <summary>Regla de negocio violada (ej. correo o VIN duplicado) -> 409 Conflict.</summary>
public class ConflictoDeNegocioException : Exception
{
    public ConflictoDeNegocioException(string mensaje) : base(mensaje) { }
}
