using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using FollowMotors.Application.Common.Interfaces;
using FollowMotors.Domain.Entities;

namespace FollowMotors.Infrastructure.Security;

public class JwtTokenService : IJwtTokenService
{
    private readonly JwtSettings _settings;

    public JwtTokenService(IOptions<JwtSettings> settings)
    {
        _settings = settings.Value;
    }

    public string GenerarToken(Usuario usuario)
    {
        var claves = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.ClaveSecreta));
        var credenciales = new SigningCredentials(claves, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
            new(ClaimTypes.Email, usuario.Correo),
            new(ClaimTypes.Role, usuario.Rol.ToString()),
            new("nombreCompleto", $"{usuario.Nombre} {usuario.Apellidos}")
        };

        if (!string.IsNullOrEmpty(usuario.NumeroCliente))
        {
            claims.Add(new Claim("numeroCliente", usuario.NumeroCliente));
        }

        var token = new JwtSecurityToken(
            issuer: _settings.Emisor,
            audience: _settings.Audiencia,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_settings.MinutosExpiracion),
            signingCredentials: credenciales);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
