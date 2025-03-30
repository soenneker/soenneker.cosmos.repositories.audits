using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Soenneker.Cosmos.Container.Abstract;
using Soenneker.Cosmos.Repositories.Audits.Abstract;
using Soenneker.Cosmos.Repository;
using Soenneker.Documents.Audit;
using Soenneker.Extensions.ValueTask;
using Soenneker.Utils.BackgroundQueue.Abstract;
using Soenneker.Utils.UserContext.Abstract;

namespace Soenneker.Cosmos.Repositories.Audits;

/// <inheritdoc cref="IAuditsRepository"/>
public sealed class AuditsRepository : CosmosRepository<AuditDocument>, IAuditsRepository
{
    public override string ContainerName => "audits";

    public override bool AuditEnabled => false;

    public AuditsRepository(ICosmosContainerUtil cosmosContainerUtil, IConfiguration config, ILogger<AuditsRepository> logger, IUserContext userContext,
        IBackgroundQueue backgroundQueue) : base(cosmosContainerUtil, config, logger, userContext, backgroundQueue)
    {
    }

    public async ValueTask<List<AuditDocument>?> GetByEntityId(string entityId, CancellationToken cancellationToken = default)
    {
        IQueryable<AuditDocument> query = await BuildQueryable(null, cancellationToken).NoSync();
        query = query.Where(x => x.PartitionKey == entityId);
        return await GetItems(query, cancellationToken: cancellationToken).NoSync();
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