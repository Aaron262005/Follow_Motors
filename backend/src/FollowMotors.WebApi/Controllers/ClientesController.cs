using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FollowMotors.Application.Clientes.Commands.ActualizarCliente;
using FollowMotors.Application.Clientes.Commands.RegistrarCliente;
using FollowMotors.Application.Clientes.DTOs;
using FollowMotors.Application.Clientes.Queries.ObtenerClientePorId;
using FollowMotors.Application.Clientes.Queries.ObtenerClientes;

namespace FollowMotors.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Gerente")]
public class ClientesController : ControllerBase
{
    private readonly IMediator _mediator;

    public ClientesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<List<ClienteDto>>> ObtenerTodos(
        [FromQuery] string? busqueda, CancellationToken cancellationToken)
    {
        var resultado = await _mediator.Send(new ObtenerClientesQuery(busqueda), cancellationToken);
        return Ok(resultado);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ClienteDto>> ObtenerPorId(Guid id, CancellationToken cancellationToken)
    {
        var resultado = await _mediator.Send(new ObtenerClientePorIdQuery(id), cancellationToken);
        return Ok(resultado);
    }

    [HttpPost]
    public async Task<ActionResult<ClienteDto>> Registrar(RegistrarClienteCommand command, CancellationToken cancellationToken)
    {
        var resultado = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(ObtenerPorId), new { id = resultado.Id }, resultado);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<ClienteDto>> Actualizar(
        Guid id, ActualizarClienteCommand command, CancellationToken cancellationToken)
    {
        var resultado = await _mediator.Send(command, cancellationToken);
        return Ok(resultado);
    }
}
