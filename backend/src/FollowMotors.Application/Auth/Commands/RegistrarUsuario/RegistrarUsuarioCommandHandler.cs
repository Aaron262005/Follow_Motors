using MediatR;
using FollowMotors.Application.Common.Exceptions;
using FollowMotors.Application.Common.Interfaces;
using FollowMotors.Domain.Entities;
using FollowMotors.Domain.Enums;
using FollowMotors.Domain.Interfaces;

namespace FollowMotors.Application.Auth.Commands.RegistrarUsuario;

public class RegistrarUsuarioCommandHandler : IRequestHandler<RegistrarUsuarioCommand, Guid>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHasher _passwordHasher;

    public RegistrarUsuarioCommandHandler(IUnitOfWork unitOfWork, IPasswordHasher passwordHasher)
    {
        _unitOfWork = unitOfWork;
        _passwordHasher = passwordHasher;
    }

    public async Task<Guid> Handle(RegistrarUsuarioCommand request, CancellationToken cancellationToken)
    {
        if (await _unitOfWork.Usuarios.ExisteCorreoAsync(request.Correo, cancellationToken))
        {
            throw new ConflictoDeNegocioException("Ya existe una cuenta registrada con ese correo.");
        }

        var numeroCliente = await _unitOfWork.Usuarios.GenerarSiguienteNumeroClienteAsync(cancellationToken);

        var usuario = new Usuario
        {
            Nombre = request.Nombre.Trim(),
            Apellidos = request.Apellidos.Trim(),
            Correo = request.Correo.Trim().ToLowerInvariant(),
            PasswordHash = _passwordHasher.Hash(request.Password),
            Rol = RolUsuario.Cliente,
            NumeroCliente = numeroCliente
        };

        await _unitOfWork.Usuarios.AgregarAsync(usuario, cancellationToken);
        await _unitOfWork.GuardarCambiosAsync(cancellationToken);

        return usuario.Id;
    }
}
