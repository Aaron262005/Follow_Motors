using MediatR;
using FollowMotors.Application.Clientes.DTOs;
using FollowMotors.Domain.Interfaces;

namespace FollowMotors.Application.Clientes.Queries.ObtenerClientes;

/// <summary>
/// Query = solo lectura, nunca modifica estado (principio CQRS). No necesita
/// Validator porque no hay reglas de negocio que validar, solo un filtro opcional.
/// </summary>
public class ObtenerClientesQueryHandler : IRequestHandler<ObtenerClientesQuery, List<ClienteDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public ObtenerClientesQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<ClienteDto>> Handle(ObtenerClientesQuery request, CancellationToken cancellationToken)
    {
        var clientes = await _unitOfWork.Usuarios.ObtenerTodosAsync(cancellationToken);

        var filtro = request.TextoBusqueda?.Trim().ToLowerInvariant();

        return clientes
            .Where(u => u.Rol == Domain.Enums.RolUsuario.Cliente)
            .Where(u => string.IsNullOrEmpty(filtro)
                || u.Nombre.ToLowerInvariant().Contains(filtro)
                || u.Apellidos.ToLowerInvariant().Contains(filtro)
                || u.Correo.ToLowerInvariant().Contains(filtro)
                || (u.NumeroCliente ?? "").ToLowerInvariant().Contains(filtro))
            .Select(u => new ClienteDto(
                u.Id, u.Nombre, u.Apellidos, u.Correo, u.Telefono,
                u.NumeroCliente, u.Activo, u.Unidades.Count))
            .ToList();
    }
}
