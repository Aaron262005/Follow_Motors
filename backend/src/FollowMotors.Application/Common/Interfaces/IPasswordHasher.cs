namespace FollowMotors.Application.Common.Interfaces;

public interface IPasswordHasher
{
    string Hash(string passwordPlano);
    bool Verificar(string passwordPlano, string passwordHash);
}
