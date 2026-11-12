using MediatR;
using FollowMotors.Application.Common.Exceptions;
using FollowMotors.Application.Unidades.DTOs;
using FollowMotors.Domain.Entities;
using FollowMotors.Domain.Enums;
using FollowMotors.Domain.Interfaces;

namespace FollowMotors.Application.Unidades.Commands.RegistrarUnidad;

public class RegistrarUnidadCommandHandler : IRequestHandler<RegistrarUnidadCommand, UnidadDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public RegistrarUnidadCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<UnidadDto> Handle(RegistrarUnidadCommand request, CancellationToken cancellationToken)
    {
        var cliente = await _unitOfWork.Usuarios.ObtenerPorIdAsync(request.ClienteId, cancellationToken)
            ?? throw new NotFoundException(nameof(Usuario), request.ClienteId);

        if (await _unitOfWork.Unidades.ExisteNumeroSerieAsync(request.NumeroSerie, cancellationToken))
        {
            throw new ConflictoDeNegocioException($"Ya existe una unidad registrada con el VIN {request.NumeroSerie}.");
        }

        var unidad = new Unidad
        {
            ClienteId = cliente.Id,
            NumeroSerie = request.NumeroSerie.Trim().ToUpperInvariant(),
            Categoria = Enum.Parse<CategoriaUnidad>(request.Categoria, ignoreCase: true),
            Modelo = request.Modelo.Trim(),
            FechaCompra = request.FechaCompra,
            FechaUltimoServicio = request.FechaUltimoServicio,
            KilometrajeUltimoServicio = request.KilometrajeUltimoServicio,
            ImagenUrl = request.ImagenUrl,
            ServicioActual = NumeroServicio.Primero
        };

        await _unitOfWork.Unidades.AgregarAsync(unidad, cancellationToken);
        await _unitOfWork.GuardarCambiosAsync(cancellationToken);

        return Mapear(unidad);
    }

    private static UnidadDto Mapear(Unidad u) => new(
        u.Id, u.ClienteId, u.NumeroSerie, u.Categoria.ToString(), u.Modelo,
        u.FechaCompra, u.ServicioActual.ToString(), u.FechaUltimoServicio,
        u.KilometrajeUltimoServicio, u.ImagenUrl);
}
