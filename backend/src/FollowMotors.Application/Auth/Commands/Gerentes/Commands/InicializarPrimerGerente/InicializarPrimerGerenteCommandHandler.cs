
using MediatR;
using FollowMotors.Application.Common.Exceptions;
using FollowMotors.Application.Common.Interfaces;
using FollowMotors.Domain.Entities;
using FollowMotors.Domain.Enums;
using FollowMotors.Domain.Interfaces;

namespace FollowMotors.Application.Gerentes.Commands.InicializarPrimerGerente;

public sealed class InicializarPrimerGerenteCommandHandler
    : IRequestHandler<InicializarPrimerGerenteCommand, Guid>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHasher _passwordHasher;

    public InicializarPrimerGerenteCommandHandler(
        IUnitOfWork unitOfWork,
        IPasswordHasher passwordHasher)
    {
        _unitOfWork = unitOfWork;
        _passwordHasher = passwordHasher;
    }

    public async Task<Guid> Handle(
        InicializarPrimerGerenteCommand request,
        CancellationToken cancellationToken)
    {
        // 1. Comprobar si existe un gerente.
        var existeGerente = await _unitOfWork.Usuarios
            .ExisteGerenteAsync(cancellationToken);

        if (existeGerente)
        {
            throw new ConflictoDeNegocioException(
                "Ya existe un gerente registrado.");
        }

        // 2. Normalizar el correo.
        var correo = request.Correo.Trim().ToLowerInvariant();

        // 3. Verificar que el correo no exista.
        var existeCorreo = await _unitOfWork.Usuarios
            .ExisteCorreoAsync(correo, cancellationToken);

        if (existeCorreo)
        {
            throw new ConflictoDeNegocioException(
                "El correo ya esta registrado.");
        }

        // 4. Crear el primer gerente.
        var gerente = new Usuario
        {
            Nombre = request.Nombre.Trim(),
            Apellidos = request.Apellidos.Trim(),
            Correo = correo,
            Telefono = string.Empty,
            PasswordHash = _passwordHasher.Hash(request.Password),
            Rol = RolUsuario.Gerente,
            NumeroCliente = null,
            Activo = true
        };

        // 5. Registrar el usuario mediante Repository.
        await _unitOfWork.Usuarios
            .AgregarAsync(gerente, cancellationToken);

        // 6. Crear auditoria.
        var auditoria = new AuditoriaGerente
        {
            ActorUsuarioId = null,
            GerenteId = gerente.Id,
            Accion = "PrimerGerenteCreado",
            FechaUtc = DateTime.UtcNow
        };

        // 7. Registrar auditoria mediante Repository.
        await _unitOfWork.AuditoriaGerentes
            .AgregarAsync(auditoria, cancellationToken);

        // 8. Guardar los cambios.
        await _unitOfWork.GuardarCambiosAsync(
            cancellationToken);

        // 9. Devolver el identificador.
        return gerente.Id;
    }
}
