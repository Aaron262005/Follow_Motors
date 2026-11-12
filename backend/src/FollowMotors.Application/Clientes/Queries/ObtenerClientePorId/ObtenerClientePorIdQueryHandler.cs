using MediatR;
using FollowMotors.Application.Clientes.DTOs;
using FollowMotors.Application.Common.Exceptions;
using FollowMotors.Domain.Interfaces;

namespace FollowMotors.Application.Clientes.Queries.ObtenerClientePorId;

public class ObtenerClientePorIdQueryHandler : IRequestHandler<ObtenerClientePorIdQuery, ClienteDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public ObtenerClientePorIdQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ClienteDto> Handle(ObtenerClientePorIdQuery request, CancellationToken cancellationToken)
    {
        var cliente = await _unitOfWork.Usuarios.ObtenerPorIdAsync(request.Id, cancellationToken)
            ?? throw new NotFoundException(nameof(Domain.Entities.Usuario), request.Id);

        return new ClienteDto(
            cliente.Id, cliente.Nombre, cliente.Apellidos, cliente.Correo,
            cliente.Telefono, cliente.NumeroCliente, cliente.Activo, cliente.Unidades.Count);
    }
}
