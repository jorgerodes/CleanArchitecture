# Clean Architecture en .NET

Repositorio de referencia que muestra una **estructura mínima y pragmática** de Clean Architecture en **.NET 8**: separación de responsabilidades, testabilidad y evolución segura.

## Estructura

```text
/src/Domain         Entidades, Value Objects, Reglas de dominio, Eventos
/src/Application    Casos de uso (interactores), Puertos (interfaces), DTO/Mapeos, Validación
/src/Infrastructure Implementaciones de puertos (EF Core, servicios externos), Repos, Migraciones
/src/Web            API/MVC, Controladores/Endpoints, DI, Middleware
/tests              Unit tests (Domain/Application) e Integración (Infrastructure/Web)
```

## Principios clave

* **Independencia del dominio**: `Domain` no depende de ningún framework.
* **Dependencias hacia adentro**: las capas externas conocen a las internas, nunca al revés.
* **Casos de uso orquestan** la lógica de aplicación; IO a través de **puertos**.
* **Infraestructura reemplazable** y **mockeable** para pruebas.
* **Boundaries explícitos** (DTOs, mapeos) y **transacciones por caso de uso**.

## Tecnologías (opcionales)

* EF Core + Migrations (persistencia)
* MediatR (orquestación de solicitudes)
* FluentValidation (validación)
* AutoMapper (mapeos)


