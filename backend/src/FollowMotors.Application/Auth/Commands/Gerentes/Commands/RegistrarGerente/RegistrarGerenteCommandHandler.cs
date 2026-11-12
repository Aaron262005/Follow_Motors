
using MediatR;
using FollowMotors.Application.Common.Exceptions;
using FollowMotors.Application.Common.Interfaces;
using FollowMotors.Domain.Entities;
using FollowMotors.Domain.Enums;
using FollowMotors.Domain.Interfaces;

namespace FollowMotors.Application.Gerentes.Commands.RegistrarGerente;

public sealed class RegistrarGerenteCommandHandler
    : IRequestHandler<RegistrarGerenteCommand, Guid>
{
    private readonly IUnitOfWork _unitOfWork; // proporciona acceso a los repos 
    private readonly ICurrentUserService _usuarioActual; // optiene la identidad del usuario autenticado 
    private readonly IPasswordHasher _hasher; // genera un hash seguro para la contraseña 

    public RegistrarGerenteCommandHandler(
        IUnitOfWork unitOfWork,
        ICurrentUserService usuarioActual,
        IPasswordHasher hasher)
    {
        _unitOfWork = unitOfWork;
        _usuarioActual = usuarioActual;
        _hasher = hasher;
    }

    public async Task<Guid> Handle(
        RegistrarGerenteCommand request,
        CancellationToken cancellationToken)
    {
        // 1. Obtener el usuario autenticado.
        var actorId = _usuarioActual.UsuarioId;

        if (actorId is null)
        {
            throw new UnauthorizedAccessException(
                "No autorizado.");
        }

        // 2. Comprobar el rol y estado actual.
        var actor = await _unitOfWork.Usuarios
            .ObtenerPorIdAsync(
                actorId.Value,
                cancellationToken);

        if (actor is null ||
            !actor.Activo ||
            actor.Rol != RolUsuario.Gerente)
        {
            throw new UnauthorizedAccessException(
                "No autorizado.");
        }

        // 3. Comprobar que el correo no exista.
        var existeCorreo = await _unitOfWork.Usuarios
            .ExisteCorreoAsync(
                request.Correo.Trim().ToLowerInvariant(),
                cancellationToken);

        if (existeCorreo)
        {
            throw new ConflictoDeNegocioException(
                "Ya existe una cuenta con este correo.");
        }

        // 4. Crear al nuevo gerente.
        var gerente = new Usuario
        {
            Nombre = request.Nombre.Trim(),
            Apellidos = request.Apellidos.Trim(),
            Correo = request.Correo.Trim().ToLowerInvariant(),
            PasswordHash = _hasher.Hash(request.Password),
            Rol = RolUsuario.Gerente,
            NumeroCliente = null,
            Activo = true
        };

        // 5. Agregar el gerente al repositorio.
        await _unitOfWork.Usuarios
            .AgregarAsync(gerente, cancellationToken);

        // 6. Registrar la auditoria del alta.
        var auditoria = new AuditoriaGerente
        {
            ActorUsuarioId = actorId,
            GerenteId = gerente.Id,
            Accion = "AltaPorGerente"
        };

        await _unitOfWork.AuditoriaGerentes
            .AgregarAsync(auditoria, cancellationToken);

        // 7. Guardar ambos registros en MySQL.
        await _unitOfWork.GuardarCambiosAsync(
            cancellationToken);

        // 8. Devolver el ID del nuevo gerente.
        return gerente.Id;
    }
}
