using Soenneker.Cosmos.Repository.Abstract;
using Soenneker.Documents.Audit;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Cosmos.Repositories.Audits.Abstract;


/// <summary>
///  Audit records aren't accessible to external resources for mutation.  This is essentially a readonly repository.
/// </summary>
public interface IAuditsRepository : ICosmosRepository<AuditDocument>
{
    [Pure]
    ValueTask<List<AuditDocument>?> GetByEntityId(string partitionKey, CancellationToken cancellationToken = default);

    /// <summary>
    /// **DO NOT CALL** Hides underlying implementation
    /// </summary>
    /// <remarks>"Audit records may not be added explicitly."</remarks>
    [Obsolete("Not supported", true)]
    new ValueTask<string> AddItem(AuditDocument document, bool useQueue = false, bool excludeResponse = false,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// **DO NOT CALL** Hides underlying implementation
    /// </summary>
    /// <remarks>"Audit records may not be updated."</remarks>
    [Obsolete("Not supported", true)]
    new ValueTask UpdateItem(string id, AuditDocument document, bool useQueue = false, bool excludeResponse = false, CancellationToken cancellationToken = default);

    /// <summary>
    /// **DO NOT CALL** Hides underlying implementation
    /// </summary>
    /// <remarks>"Audit records may not be deleted."</remarks>
    [Obsolete("Not supported", true)]
    new ValueTask DeleteItem(string id, bool useQueue = false, CancellationToken cancellationToken = default);
}