# ModularMonolithTemplate

An ASP.NET Core modular monolith built with .NET 10 and Aspire.

## Architecture

The solution is organized as a modular monolith: the API hosts multiple feature modules in a single deployable application, while each module keeps its own endpoints, services, persistence, and public contracts.

- **Reminder module**: owns the todo/reminder workflow, EF Core persistence, and related API endpoints.
- **Weather module**: demonstrates a separate feature module that can call into shared/public module contracts.
- **Cross-cutting platform services**: Aspire hosts SQL Server, Redis, RabbitMQ, and Seq for local development; the API uses shared service defaults for logging, health checks, service discovery, and resilience.

Modules communicate through public abstractions and messaging rather than direct coupling to internal implementation details.

## What is included

- `src/ModularMonolithTemplate.Api`: the main HTTP API
- `src/ModularMonolithTemplate.AppHost`: the Aspire app host for local orchestration
- `src/ModularMonolithTemplate.ServiceDefaults`: shared hosting, observability, and resiliency defaults
- `tests/`: unit and integration test projects

## Prerequisites

- .NET 10 SDK
- Docker Desktop

## Run locally

Start the Aspire app host:

```powershell
dotnet run --project src/ModularMonolithTemplate.AppHost
```

The app host starts the API and supporting services such as SQL Server, Redis, RabbitMQ, and Seq.

## Open the API

- Swagger UI: `https://localhost:7103/swagger`
- HTTP fallback: `http://localhost:5157/swagger`

## Configuration

- `src/ModularMonolithTemplate.AppHost/appsettings.json` contains local Aspire parameters
- `src/ModularMonolithTemplate.Api/appsettings.json` contains API logging and service settings
- Secrets such as SQL passwords should be stored with user secrets for local development

## Solution structure

Open `ModularMonolithTemplate.sln` in Visual Studio or Rider to work with the full solution.
