using MediatR;
using FollowMotors.Application.Unidades.DTOs;
using FollowMotors.Domain.Interfaces;

namespace FollowMotors.Application.Unidades.Queries.ObtenerUnidadesPorCliente;

public class ObtenerUnidadesPorClienteQueryHandler
    : IRequestHandler<ObtenerUnidadesPorClienteQuery, List<UnidadDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public ObtenerUnidadesPorClienteQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<UnidadDto>> Handle(ObtenerUnidadesPorClienteQuery request, CancellationToken cancellationToken)
    {
        var unidades = await _unitOfWork.Unidades.ObtenerPorClienteIdAsync(request.ClienteId, cancellationToken);

        return unidades.Select(u => new UnidadDto(
            u.Id, u.ClienteId, u.NumeroSerie, u.Categoria.ToString(), u.Modelo,
            u.FechaCompra, u.ServicioActual.ToString(), u.FechaUltimoServicio,
            u.KilometrajeUltimoServicio, u.ImagenUrl)).ToList();
    }
}
