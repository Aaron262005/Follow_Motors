using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FollowMotors.Application.Unidades.Commands.RegistrarUnidad;
using FollowMotors.Application.Unidades.DTOs;
using FollowMotors.Application.Unidades.Queries.ObtenerUnidadesPorCliente;

namespace FollowMotors.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class UnidadesController : ControllerBase
{
    private readonly IMediator _mediator;

    public UnidadesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("cliente/{clienteId:guid}")]
    public async Task<ActionResult<List<UnidadDto>>> ObtenerPorCliente(Guid clienteId, CancellationToken cancellationToken)
    {
        var resultado = await _mediator.Send(new ObtenerUnidadesPorClienteQuery(clienteId), cancellationToken);
        return Ok(resultado);
    }

    [HttpPost]
    [Authorize(Roles = "Gerente")]
    public async Task<ActionResult<UnidadDto>> Registrar(RegistrarUnidadCommand command, CancellationToken cancellationToken)
    {
        var resultado = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(ObtenerPorCliente), new { clienteId = resultado.ClienteId }, resultado);
    }
}
