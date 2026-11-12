using MediatR;
using FollowMotors.Application.Clientes.DTOs;
using FollowMotors.Application.Common.Interfaces;
using FollowMotors.Domain.Entities;
using FollowMotors.Domain.Enums;
using FollowMotors.Domain.Interfaces;

namespace FollowMotors.Application.Clientes.Commands.RegistrarCliente;

public class RegistrarClienteCommandHandler : IRequestHandler<RegistrarClienteCommand, ClienteDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHasher _passwordHasher;

    public RegistrarClienteCommandHandler(IUnitOfWork unitOfWork, IPasswordHasher passwordHasher)
    {
        _unitOfWork = unitOfWork;
        _passwordHasher = passwordHasher;
    }

    public async Task<ClienteDto> Handle(RegistrarClienteCommand request, CancellationToken cancellationToken)
    {
        var existente = await _unitOfWork.Usuarios.ObtenerPorCorreoAsync(request.Correo, cancellationToken);

        if (existente is not null)
        {
            // El gerente puede registrar una unidad para un cliente que ya existe;
            // en ese caso no se duplica, solo se devuelve.
            return Mapear(existente);
        }

        var numeroCliente = await _unitOfWork.Usuarios.GenerarSiguienteNumeroClienteAsync(cancellationToken);

        var passwordPlano = string.IsNullOrWhiteSpace(request.Password)
            ? Guid.NewGuid().ToString("N")[..10] // contrasena temporal si el gerente no da una
            : request.Password;

        var cliente = new Usuario
        {
            Nombre = request.Nombre.Trim(),
            Apellidos = request.Apellidos.Trim(),
            Correo = request.Correo.Trim().ToLowerInvariant(),
            Telefono = request.Telefono.Trim(),
            PasswordHash = _passwordHasher.Hash(passwordPlano),
            Rol = RolUsuario.Cliente,
            NumeroCliente = numeroCliente
        };

        await _unitOfWork.Usuarios.AgregarAsync(cliente, cancellationToken);
        await _unitOfWork.GuardarCambiosAsync(cancellationToken);

        return Mapear(cliente);
    }

    private static ClienteDto Mapear(Usuario usuario) => new(
        usuario.Id,
        usuario.Nombre,
        usuario.Apellidos,
        usuario.Correo,
        usuario.Telefono,
        usuario.NumeroCliente,
        usuario.Activo,
        usuario.Unidades.Count);
}
