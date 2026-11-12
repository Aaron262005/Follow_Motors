using MediatR;
using FollowMotors.Application.Common.Interfaces;
using FollowMotors.Domain.Interfaces;

namespace FollowMotors.Application.Auth.Commands.Login;

/// <summary>
/// Toda la logica de negocio del login vive aqui, nunca en el Controller.
/// Depende solo de abstracciones (IUnitOfWork, IPasswordHasher, IJwtTokenService),
/// nunca de EF Core directamente (Dependency Inversion).
/// </summary>
public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResult>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtTokenService _jwtTokenService;

    public LoginCommandHandler(
        IUnitOfWork unitOfWork,
        IPasswordHasher passwordHasher,
        IJwtTokenService jwtTokenService)
    {
        _unitOfWork = unitOfWork;
        _passwordHasher = passwordHasher;
        _jwtTokenService = jwtTokenService;
    }

    public async Task<LoginResult> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var usuario = await _unitOfWork.Usuarios.ObtenerPorCorreoAsync(request.Correo, cancellationToken);

        if (usuario is null || !usuario.Activo || !_passwordHasher.Verificar(request.Password, usuario.PasswordHash))
        {
            throw new UnauthorizedAccessException("Correo o contrasena incorrectos.");
        }

        var token = _jwtTokenService.GenerarToken(usuario);

        return new LoginResult(
            token,
            usuario.Id,
            $"{usuario.Nombre} {usuario.Apellidos}",
            usuario.Rol.ToString(),
            usuario.NumeroCliente);
    }
}
