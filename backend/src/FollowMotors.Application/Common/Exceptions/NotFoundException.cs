namespace FollowMotors.Application.Common.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string entidad, object clave)
        : base($"\"{entidad}\" con clave ({clave}) no fue encontrado(a).")
    {
    }
}
