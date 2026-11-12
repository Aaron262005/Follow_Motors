using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using FollowMotors.Application;
using FollowMotors.Infrastructure;
using FollowMotors.Infrastructure.Security;
using FollowMotors.WebApi.Middleware;
using MediatR;
using FollowMotors.Application.Gerentes.Commands.InicializarPrimerGerente;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(opciones =>
{
    opciones.AddPolicy("Angular", politica =>
    {
        // Ajusta el origen al puerto real de `ng serve` (4200 por defecto).
        politica.WithOrigins(builder.Configuration["FrontendUrl"] ?? "http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

var jwtSettings = builder.Configuration.GetSection(JwtSettings.SeccionConfiguracion).Get<JwtSettings>()
    ?? new JwtSettings();

builder.Services
    .AddAuthentication(opciones =>
    {
        opciones.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        opciones.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(opciones =>
    {
        opciones.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings.Emisor,
            ValidAudience = jwtSettings.Audiencia,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.ClaveSecreta))
        };
    });

builder.Services.AddAuthorization();

var app = builder.Build();

if (args.Contains(
    "--bootstrap-gerente",
    StringComparer.OrdinalIgnoreCase))
{
// 1. Solo se permite en desarrollo.
    if (!app.Environment.IsDevelopment())
    {
        throw new InvalidOperationException(
            "La inicializacion solo esta permitida en Development.");
    }

    // 2. Exigir el proveedor MySQL configurado.
    if (!string.Equals(
        builder.Configuration["DatabaseProvider"],
        "MySql",
        StringComparison.OrdinalIgnoreCase))
    {
        throw new InvalidOperationException(
            "La inicializacion requiere MySQL.");
    }

    // 3. Obtener los datos del entorno local.
    var nombre = Environment.GetEnvironmentVariable(
        "FM_BOOTSTRAP_NOMBRE");

    var apellidos = Environment.GetEnvironmentVariable(
        "FM_BOOTSTRAP_APELLIDOS");

    var correo = Environment.GetEnvironmentVariable(
        "FM_BOOTSTRAP_CORREO");

    var password = Environment.GetEnvironmentVariable(
        "FM_BOOTSTRAP_PASSWORD");

    // 4. Comprobar que los datos esten presentes.
    if (string.IsNullOrWhiteSpace(nombre) ||
        string.IsNullOrWhiteSpace(apellidos) ||
        string.IsNullOrWhiteSpace(correo) ||
        string.IsNullOrWhiteSpace(password))
    {
        throw new InvalidOperationException(
            "Faltan los datos para inicializar el primer gerente.");
    }

    // 5. Evitar dos inicializaciones simultaneas
    // en el mismo equipo.
    var rutaBloqueo = Path.Combine(
        Path.GetTempPath(),
        "FollowMotors_PrimerGerente.lock");

    FileStream archivoBloqueo;

    try
    {
        archivoBloqueo = new FileStream(
            rutaBloqueo,
            FileMode.OpenOrCreate,
            FileAccess.ReadWrite,
            FileShare.None);
    }
    catch (IOException ex)
    {
        throw new InvalidOperationException(
            "Otra inicializacion local esta en proceso.",
            ex);
    }

    await using (archivoBloqueo)
    {
        // 6. Crear un alcance de dependencias.
        await using var scope =
            app.Services.CreateAsyncScope();

        // 7. Obtener MediatR.
        var mediator = scope.ServiceProvider
            .GetRequiredService<ISender>();

        // 8. Enviar el Command al Handler.
        var command = new InicializarPrimerGerenteCommand(
            nombre,
            apellidos,
            correo,
            password,
            password);

        var gerenteId = await mediator.Send(command);

        // 9. Mostrar solamente el identificador.
        Console.WriteLine(
            $"Primer gerente creado. Id: {gerenteId}");
    }

    // 10. Terminar sin iniciar el servidor HTTP.
    return;
}


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandling();
app.UseHttpsRedirection();
app.UseCors("Angular");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
