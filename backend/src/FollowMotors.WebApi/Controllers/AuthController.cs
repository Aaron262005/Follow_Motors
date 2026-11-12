using MediatR;
using Microsoft.AspNetCore.Mvc;
using FollowMotors.Application.Auth.Commands.Login;
using FollowMotors.Application.Auth.Commands.RegistrarUsuario;

namespace FollowMotors.WebApi.Controllers;


[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("login")]
    public async Task<ActionResult<LoginResult>> Login(LoginCommand command, CancellationToken cancellationToken)
    {
        var resultado = await _mediator.Send(command, cancellationToken);
        return Ok(resultado);
    }

    [HttpPost("registro")]
    public async Task<ActionResult<Guid>> Registro(RegistrarUsuarioCommand command, CancellationToken cancellationToken)
    {
        var id = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(Registro), new { id }, id);
    }
}
