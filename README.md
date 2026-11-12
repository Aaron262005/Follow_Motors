# Follow Motors — Base de arquitectura (backend CQRS + frontend MVVM)

Generado a partir del prototipo HTML/CSS "Follow Motors" (seguimiento de
servicios de motos/ATV/UTV para una agencia). Este README es la guía para
levantar el proyecto y conectar la base de datos cuando esté lista.

## Estructura

```
backend/
  FollowMotors.sln
  src/
    FollowMotors.Domain/         Entidades + interfaces (no depende de nada)
    FollowMotors.Application/    CQRS: Commands, Queries, Validators, Handlers, Mediator
    FollowMotors.Infrastructure/ EF Core, Repositories, JWT, hashing (implementa Domain)
    FollowMotors.WebApi/         Controllers delgados, Program.cs, appsettings

frontend/
  src/app/
    core/         Modelos, servicios HTTP, interceptor de JWT, guards
    features/
      auth/login, auth/registro        -> View + ViewModel (MVVM)
      clientes/clientes-list           -> módulo de ejemplo (gerente)
      unidades/mis-unidades            -> módulo de ejemplo (cliente)
```

## Flujo de una petición (CQRS)

```
Angular (View -> ViewModel) --HTTP/JSON--> Controller (sin lógica)
    --> Mediator.Send(Command)
    --> ValidationBehavior corre el Validator del Command
    --> si pasa, el Handler ejecuta la lógica de negocio
    --> Handler usa IUnitOfWork/IRepository (interfaces del Domain)
    --> Infrastructure (EF Core) persiste en la BD configurada
```

## === Base de datos: cómo conectarla cuando esté lista ===

El proyecto corre hoy con un proveedor **InMemory** (no necesita nada
instalado). Todo el código de Infrastructure ya está preparado para los
otros 3 proveedores; **no hay que tocar una sola línea de código**, solo
editar `backend/src/FollowMotors.WebApi/appsettings.json`:

```jsonc
"DatabaseProvider": "SqlServer",   // o "Postgres" / "Sqlite" / "InMemory"
"ConnectionStrings": {
  "Default": "Server=localhost;Database=FollowMotorsDb;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

Y luego crear la primera migración y aplicarla:

```bash
cd backend
dotnet ef migrations add Inicial --project src/FollowMotors.Infrastructure --startup-project src/FollowMotors.WebApi
dotnet ef database update --project src/FollowMotors.Infrastructure --startup-project src/FollowMotors.WebApi
```

(`dotnet tool install --global dotnet-ef` si no tienes la herramienta.)

## Levantar el backend

```bash
cd backend
dotnet restore
dotnet run --project src/FollowMotors.WebApi
# Swagger: http://localhost:5100/swagger
```

## Levantar el frontend

```bash
cd frontend
npm install
npm start
# http://localhost:4200
```

Revisa `src/environments/environment.ts` para que `apiUrl` apunte al puerto
real del backend.

## Qué sigue

Esta entrega cubre la base de la arquitectura + Auth (login/registro) +
el módulo de ejemplo Clientes/Unidades, de punta a punta (backend y
frontend). Los módulos restantes del prototipo (agenda de citas,
primer/segundo/tercer servicio, garantías finalizadas, clientes-cuentas
con edición completa) se agregan uno por uno siguiendo exactamente este
mismo patrón: Command/Query + Validator + Handler en el backend,
View + ViewModel en el frontend.
