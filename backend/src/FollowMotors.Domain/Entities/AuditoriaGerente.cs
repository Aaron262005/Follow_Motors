
using System;

namespace FollowMotors.Domain.Entities;

public sealed class AuditoriaGerente
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid? ActorUsuarioId { get; set; } // id del que realizo el registro 

    public Guid GerenteId { get; set; } // gerente fue creado 

    public string Accion { get; set; } = string.Empty;

    public DateTime FechaUtc { get; set; } = DateTime.UtcNow; // fecha y hora de creacion 
}
