
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using FollowMotors.Application.Gerentes.Commands.RegistrarGerente;

namespace FollowMotors.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Gerente")]
public sealed class GerentesController : ControllerBase
{
    private readonly IMediator _mediator;

    public GerentesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult> Registrar(
        [FromBody] RegistrarGerenteCommand command,
        CancellationToken cancellationToken)
    {
        var gerenteId = await _mediator.Send(
            command,
            cancellationToken);

        return StatusCode(
            StatusCodes.Status201Created,
            new { Id = gerenteId });
    }
}
