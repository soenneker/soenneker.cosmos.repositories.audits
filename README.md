[![](https://img.shields.io/nuget/v/soenneker.cosmos.repositories.audits.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.cosmos.repositories.audits/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.cosmos.repositories.audits/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.cosmos.repositories.audits/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.cosmos.repositories.audits.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.cosmos.repositories.audits/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.cosmos.repositories.audits/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.cosmos.repositories.audits/actions/workflows/codeql.yml)

# Soenneker.Cosmos.Repositories.Audits

A Cosmos DB repository specialized for `AuditDocument` records stored in the `audits` container.

## Installation

```bash
dotnet add package Soenneker.Cosmos.Repositories.Audits
```

## Registration

```csharp
using Soenneker.Cosmos.Repositories.Audits.Abstract;
using Soenneker.Cosmos.Repositories.Audits.Registrars;

services.AddAuditsRepositoryAsScoped();

IAuditsRepository audits = serviceProvider.GetRequiredService<IAuditsRepository>();
```

Use the scoped registration because the repository depends on scoped user context. `AddAuditsRepositoryAsSingleton()` remains for source compatibility but is obsolete and now also registers a scoped repository.

The registrar adds the background queue, user context, and Cosmos container dependencies. Configure the Cosmos dependencies under `Azure:Cosmos` as required by those packages.

## Get an entity's audit history

```csharp
List<AuditDocument> history = await audits.GetByEntity(
    entityId,
    cancellationToken);
```

`entityId` is used as the Cosmos partition key. The method returns every audit document in that partition; it does not apply additional ordering or pagination.

## Mutation boundary

`AddItem`, the `UpdateItem(id, ...)` overload, and the `DeleteItem(id, ...)` overload are marked obsolete with compile-time errors and throw `NotSupportedException` from the concrete repository. Audit creation is expected to happen through the audit behavior in the general Cosmos repository rather than through this specialized repository.

This API is not an authorization boundary. `IAuditsRepository` inherits the broader `ICosmosRepository<AuditDocument>` contract, which includes other mutation operations. Do not expose the repository to untrusted callers; enforce write restrictions in the application and Cosmos account permissions.

The repository disables recursive audit generation for the `audits` container. Cosmos failures and cancellation propagate according to the underlying repository operation.
