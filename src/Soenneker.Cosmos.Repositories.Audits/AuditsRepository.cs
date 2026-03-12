using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Soenneker.Cosmos.Container.Abstract;
using Soenneker.Cosmos.Repositories.Audits.Abstract;
using Soenneker.Cosmos.Repository;
using Soenneker.Documents.Audit;
using Soenneker.Utils.BackgroundQueue.Abstract;
using Soenneker.Utils.MemoryStream.Abstract;
using Soenneker.Utils.UserContext.Abstract;

namespace Soenneker.Cosmos.Repositories.Audits;

/// <inheritdoc cref="IAuditsRepository"/>
public sealed class AuditsRepository : CosmosRepository<AuditDocument>, IAuditsRepository
{
    public override string ContainerName => "audits";

    public override bool AuditEnabled => false;

    public AuditsRepository(ICosmosContainerUtil cosmosContainerUtil, IConfiguration config, ILogger<AuditsRepository> logger, IUserContext userContext,
        IBackgroundQueue backgroundQueue, IMemoryStreamUtil memoryStreamUtil) : base(cosmosContainerUtil, config, logger, userContext, backgroundQueue,
        memoryStreamUtil)
    {
    }

    public ValueTask<List<AuditDocument>> GetByEntity(string entityId, CancellationToken cancellationToken = default)
    {
        return GetAllByPartitionKey(entityId, cancellationToken: cancellationToken);
    }

    [Obsolete("Not supported", true)]
    public override ValueTask<string> AddItem(AuditDocument document, bool useQueue = false, bool excludeResponse = false,
        CancellationToken cancellationToken = default)
    {
        throw new NotSupportedException("Audit records may not be added explicitly");
    }

    [Obsolete("Not supported", true)]
    public new ValueTask UpdateItem(string id, AuditDocument document, bool useQueue = false, bool excludeResponse = false,
        CancellationToken cancellationToken = default)
    {
        throw new NotSupportedException("Audit records may not be updated");
    }

    [Obsolete("Not supported", true)]
    public new ValueTask DeleteItem(string id, bool useQueue = false, CancellationToken cancellationToken = default)
    {
        throw new NotSupportedException("Audit records may not be deleted");
    }
}