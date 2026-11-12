using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using FollowMotors.Application.Common.Interfaces;
using FollowMotors.Domain.Interfaces;
using FollowMotors.Infrastructure.Persistence;
using FollowMotors.Infrastructure.Persistence.Repositories;
using FollowMotors.Infrastructure.Security;

namespace FollowMotors.Infrastructure;

/// <summary>
/// ============================================================
///  APARTADO DE BASE DE DATOS
/// ============================================================
/// Mientras la BD real "se sigue procesando", el proyecto corre con
/// un proveedor InMemory (no necesita nada instalado ni conexion).
///
/// Cuando la BD este lista, en appsettings.json solo hay que llenar:
///
///   "DatabaseProvider": "SqlServer" | "Postgres" | "Sqlite" | "InMemory"
///   "ConnectionStrings": { "Default": "<tu cadena de conexion real>" }
///
/// y correr: dotnet ef database update --project src/FollowMotors.Infrastructure
///                                     --startup-project src/FollowMotors.WebApi
///
/// No hay que tocar ni un archivo de codigo: todo el resto de la capa
/// (repositorios, UnitOfWork, Handlers) ya trabaja contra ApplicationDbContext,
/// sin saber ni importarle que proveedor hay detras (Dependency Inversion).
/// ============================================================
/// </summary>
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection servicios,
        IConfiguration configuracion)
    {
        var proveedor = configuracion["DatabaseProvider"] ?? "InMemory";
        var cadenaConexion = configuracion.GetConnectionString("Default");

        servicios.AddDbContext<ApplicationDbContext>(opciones =>
        {
            switch (proveedor)
            {
                case "MySql":
                    opciones.UseMySql(cadenaConexion, ServerVersion.AutoDetect(cadenaConexion));
                    break;
                case "SqlServer":
                    opciones.UseSqlServer(cadenaConexion);
                    break;
                case "Postgres":
                    opciones.UseNpgsql(cadenaConexion);
                    break;
                case "Sqlite":
                    opciones.UseSqlite(cadenaConexion);
                    break;
                case "InMemory":
                default:
                    opciones.UseInMemoryDatabase("FollowMotorsDb");
                    break;
            }
        });

        servicios.AddScoped<IUsuarioRepository, UsuarioRepository>();
        servicios.AddScoped<IUnidadRepository, UnidadRepository>();
        servicios.AddScoped<IUnitOfWork, UnitOfWork>();

        servicios.AddSingleton<IPasswordHasher, PasswordHasher>();
        servicios.Configure<JwtSettings>(configuracion.GetSection(JwtSettings.SeccionConfiguracion));
        servicios.AddScoped<IJwtTokenService, JwtTokenService>();

        servicios.AddHttpContextAccessor();
        servicios.AddScoped<ICurrentUserService, CurrentUserService>();

        return servicios;
    }
}
