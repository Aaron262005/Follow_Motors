using FollowMotors.Application.Common.Interfaces;

namespace FollowMotors.Infrastructure.Security;

public class PasswordHasher : IPasswordHasher
{
    public string Hash(string passwordPlano) => BCrypt.Net.BCrypt.HashPassword(passwordPlano);

    public bool Verificar(string passwordPlano, string passwordHash) =>
        BCrypt.Net.BCrypt.Verify(passwordPlano, passwordHash);
}
