namespace FollowMotors.Application.Clientes.DTOs;

/// <summary>Lo que el frontend recibe; nunca se expone PasswordHash.</summary>
public record ClienteDto(
    Guid Id,
    string Nombre,
    string Apellidos,
    string Correo,
    string Telefono,
    string? NumeroCliente,
    bool Activo,
    int CantidadUnidades);
