using FollowMotors.Domain.Entities;

namespace FollowMotors.Application.Common.Interfaces;

public interface IJwtTokenService
{
    string GenerarToken(Usuario usuario);
}
