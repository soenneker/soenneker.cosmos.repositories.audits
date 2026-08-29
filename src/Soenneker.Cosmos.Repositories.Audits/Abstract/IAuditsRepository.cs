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
    /// <summary>
    /// Gets by entity.
    /// </summary>
    /// <param name="partitionKey">Partition key used to route the database operation.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task whose result is the collection returned by get By Entity.</returns>
    [Pure]
    ValueTask<List<AuditDocument>> GetByEntity(string partitionKey, CancellationToken cancellationToken = default);

    /// <summary>
    /// **DO NOT CALL** Hides underlying implementation
    /// </summary>
    /// <param name="document">Document to read, persist, or update.</param>
    /// <param name="useQueue">Whether to enqueue the write for background execution instead of awaiting Redis directly.</param>
    /// <param name="excludeResponse">exclude Response returned by the upstream operation.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task whose result is the text returned by add Item.</returns>
    /// <remarks>"Audit records may not be added explicitly."</remarks>
    [Obsolete("Not supported", true)]
    new ValueTask<string> AddItem(AuditDocument document, bool useQueue = false, bool excludeResponse = false, CancellationToken cancellationToken = default);

    /// <summary>
    /// **DO NOT CALL** Hides underlying implementation
    /// </summary>
    /// <param name="id">Identifier of the audits repository instance or registration to target.</param>
    /// <param name="document">Document to read, persist, or update.</param>
    /// <param name="useQueue">Whether to enqueue the write for background execution instead of awaiting Redis directly.</param>
    /// <param name="excludeResponse">exclude Response returned by the upstream operation.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task that completes when the item update is complete.</returns>
    /// <remarks>"Audit records may not be updated."</remarks>
    [Obsolete("Not supported", true)]
    new ValueTask UpdateItem(string id, AuditDocument document, bool useQueue = false, bool excludeResponse = false,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// **DO NOT CALL** Hides underlying implementation
    /// </summary>
    /// <param name="id">Identifier of the audits repository instance or registration to target.</param>
    /// <param name="useQueue">Whether to enqueue the write for background execution instead of awaiting Redis directly.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task that completes when the item deletion is complete.</returns>
    /// <remarks>"Audit records may not be deleted."</remarks>
    [Obsolete("Not supported", true)]
    new ValueTask DeleteItem(string id, bool useQueue = false, CancellationToken cancellationToken = default);
}
