
-- Base de datos de Follow Motors.
USE cfmoto;

-- Crear tabla de auditoria de gerentes.
CREATE TABLE IF NOT EXISTS AuditoriaGerentes
(
    -- Identificador unico de la auditoria.
    Id CHAR(36)
        CHARACTER SET ascii
        COLLATE ascii_general_ci
        NOT NULL,

    -- Usuario que realizo la operacion.
    ActorUsuarioId CHAR(36)
        CHARACTER SET ascii
        COLLATE ascii_general_ci
        NULL,

    -- Gerente que fue registrado.
    GerenteId CHAR(36)
        CHARACTER SET ascii
        COLLATE ascii_general_ci
        NOT NULL,

    -- Accion administrativa realizada.
    Accion VARCHAR(40) NOT NULL,

    -- Fecha y hora de la operacion.
    FechaUtc DATETIME(6) NOT NULL,

    -- Llave primaria.
    PRIMARY KEY (Id),

    -- Indice para buscar auditorias por gerente.
    INDEX IX_AuditoriaGerentes_GerenteId
        (GerenteId)

) ENGINE = InnoDB
  DEFAULT CHARSET = utf8mb4;
