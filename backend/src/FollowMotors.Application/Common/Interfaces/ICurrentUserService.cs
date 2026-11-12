namespace FollowMotors.Application.Common.Interfaces;

/// <summary>Expone al usuario autenticado actual (leido del JWT) a cualquier Handler.</summary>
public interface ICurrentUserService
{
    Guid? UsuarioId { get; }
    string? Rol { get; }
}
