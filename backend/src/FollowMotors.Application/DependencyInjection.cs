using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using FollowMotors.Application.Common.Behaviors;

namespace FollowMotors.Application;

/// <summary>
/// Punto unico de registro de la capa Application (MediatR + FluentValidation +
/// pipeline). Program.cs solo llama AddApplication(); no conoce MediatR ni
/// FluentValidation directamente (Inversion de dependencias).
/// </summary>
public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection servicios)
    {
        var ensamblado = Assembly.GetExecutingAssembly();

        servicios.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(ensamblado));
        servicios.AddValidatorsFromAssembly(ensamblado);
        servicios.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        return servicios;
    }
}
