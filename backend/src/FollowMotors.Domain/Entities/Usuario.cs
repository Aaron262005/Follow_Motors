using FollowMotors.Domain.Common;
using FollowMotors.Domain.Enums;

namespace FollowMotors.Domain.Entities;

/// <summary>
/// Cliente o gerente del taller. Equivalente al objeto "usuario" de localStorage
/// en el prototipo (registro.html / login.html / clientes-cuentas.html).
/// </summary>
public class Usuario : BaseEntity
{
    public string Nombre { get; set; } = string.Empty;
    public string Apellidos { get; set; } = string.Empty;
    public string Correo { get; set; } = string.Empty;
    public string Telefono { get; set; } = string.Empty;

    /// <summary>Hash de contraseña (BCrypt) - nunca se guarda en texto plano.</summary>
    public string PasswordHash { get; set; } = string.Empty;

    public RolUsuario Rol { get; set; } = RolUsuario.Cliente;

    /// <summary>Folio tipo "FM-0001", solo aplica a clientes (ver registrar-unidad.js / clientes-cuentas.js).</summary>
    public string? NumeroCliente { get; set; }

    public bool Activo { get; set; } = true;

    public ICollection<Unidad> Unidades { get; set; } = new List<Unidad>();
}
