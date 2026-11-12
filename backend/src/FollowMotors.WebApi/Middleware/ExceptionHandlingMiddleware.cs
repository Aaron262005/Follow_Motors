using System.Net;
using System.Text.Json;
using FollowMotors.Application.Common.Exceptions;
using ValidationException = FollowMotors.Application.Common.Exceptions.ValidationException;

namespace FollowMotors.WebApi.Middleware;

/// <summary>
/// Traduce las excepciones de Application a respuestas HTTP consistentes.
/// Los Controllers no tienen try/catch: toda esa logica vive aqui, una sola vez.
/// </summary>
public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _siguiente;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate siguiente, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _siguiente = siguiente;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext contexto)
    {
        try
        {
            await _siguiente(contexto);
        }
        catch (Exception excepcion)
        {
            _logger.LogError(excepcion, "Error no controlado procesando {Ruta}", contexto.Request.Path);
            await ManejarExcepcionAsync(contexto, excepcion);
        }
    }

    private static async Task ManejarExcepcionAsync(HttpContext contexto, Exception excepcion)
    {
        contexto.Response.ContentType = "application/json";

        var (codigo, cuerpo) = excepcion switch
        {
            ValidationException ex => (HttpStatusCode.BadRequest, (object)new { mensaje = ex.Message, errores = ex.Errores }),
            NotFoundException ex => (HttpStatusCode.NotFound, new { mensaje = ex.Message }),
            ConflictoDeNegocioException ex => (HttpStatusCode.Conflict, new { mensaje = ex.Message }),
            UnauthorizedAccessException ex => (HttpStatusCode.Unauthorized, new { mensaje = ex.Message }),
            _ => (HttpStatusCode.InternalServerError, new { mensaje = "Ocurrio un error inesperado en el servidor." })
        };

        contexto.Response.StatusCode = (int)codigo;
        await contexto.Response.WriteAsync(JsonSerializer.Serialize(cuerpo));
    }
}

public static class ExceptionHandlingMiddlewareExtensions
{
    public static IApplicationBuilder UseExceptionHandling(this IApplicationBuilder app) =>
        app.UseMiddleware<ExceptionHandlingMiddleware>();
}
