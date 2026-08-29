[![](https://img.shields.io/nuget/v/soenneker.cosmos.repositories.audits.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.cosmos.repositories.audits/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.cosmos.repositories.audits/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.cosmos.repositories.audits/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.cosmos.repositories.audits.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.cosmos.repositories.audits/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.cosmos.repositories.audits/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.cosmos.repositories.audits/actions/workflows/codeql.yml)

# Soenneker.Cosmos.Repositories.Audits

Audit records aren't accessible to external resources for mutation. This is essentially a readonly repository.

## Install

```bash
dotnet add package Soenneker.Cosmos.Repositories.Audits
```

## Quick start

```csharp
using Soenneker.Cosmos.Repositories.Audits.Registrars;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();
var result = services.AddAuditsRepositoryAsSingleton();
```

Adds `IAuditsRepository` as a singleton service.

## What you get

- `IAuditsRepository` — Audit records aren't accessible to external resources for mutation. This is essentially a readonly repository.
- `AuditsRepositoryRegistrar` — A data persistence abstraction layer for Cosmos DB Audit type documents.

## API at a glance

| API | What it does | Result / important behavior |
| --- | --- | --- |
| `IAuditsRepository.AddItem(document, useQueue, excludeResponse, cancellationToken)` | **DO NOT CALL** Hides underlying implementation. | A task whose result is the text returned by add Item. |
| `IAuditsRepository.UpdateItem(id, document, useQueue, excludeResponse, cancellationToken)` | **DO NOT CALL** Hides underlying implementation. | A task that completes when the item update is complete. |
| `IAuditsRepository.DeleteItem(id, useQueue, cancellationToken)` | **DO NOT CALL** Hides underlying implementation. | A task that completes when the item deletion is complete. |
| `AuditsRepositoryRegistrar.AddAuditsRepositoryAsSingleton(services)` | Adds `IAuditsRepository` as a singleton service. | The same service collection, so additional registrations can be chained. |
| `AuditsRepositoryRegistrar.AddAuditsRepositoryAsScoped(services)` | Adds `IAuditsRepository` as a scoped service. | The same service collection, so additional registrations can be chained. |

## Important behavior

- `IAuditsRepository.AddItem(document, useQueue, excludeResponse, cancellationToken)`: "Audit records may not be added explicitly.".
- `IAuditsRepository.UpdateItem(id, document, useQueue, excludeResponse, cancellationToken)`: "Audit records may not be updated.".
- `IAuditsRepository.DeleteItem(id, useQueue, cancellationToken)`: "Audit records may not be deleted.".

## Practical notes

- Cancellation stops pending work; it does not undo work that has already completed.
