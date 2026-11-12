using MediatR;
using FollowMotors.Application.Clientes.DTOs;
using FollowMotors.Application.Common.Exceptions;
using FollowMotors.Domain.Interfaces;

namespace FollowMotors.Application.Clientes.Commands.ActualizarCliente;

public class ActualizarClienteCommandHandler : IRequestHandler<ActualizarClienteCommand, ClienteDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public ActualizarClienteCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ClienteDto> Handle(ActualizarClienteCommand request, CancellationToken cancellationToken)
    {
        var cliente = await _unitOfWork.Usuarios.ObtenerPorIdAsync(request.Id, cancellationToken)
            ?? throw new NotFoundException(nameof(Domain.Entities.Usuario), request.Id);

        cliente.Nombre = request.Nombre.Trim();
        cliente.Apellidos = request.Apellidos.Trim();
        cliente.Telefono = request.Telefono.Trim();
        cliente.Correo = request.Correo.Trim().ToLowerInvariant();
        cliente.FechaActualizacion = DateTime.UtcNow;

        _unitOfWork.Usuarios.Actualizar(cliente);
        await _unitOfWork.GuardarCambiosAsync(cancellationToken);

        return new ClienteDto(
            cliente.Id, cliente.Nombre, cliente.Apellidos, cliente.Correo,
            cliente.Telefono, cliente.NumeroCliente, cliente.Activo, cliente.Unidades.Count);
    }
}
