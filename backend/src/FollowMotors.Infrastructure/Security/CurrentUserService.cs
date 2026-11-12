using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using FollowMotors.Application.Common.Interfaces;

namespace FollowMotors.Infrastructure.Security;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid? UsuarioId
    {
        get
        {
            var valor = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            return Guid.TryParse(valor, out var id) ? id : null;
        }
    }

    public string? Rol => _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Role);
}
