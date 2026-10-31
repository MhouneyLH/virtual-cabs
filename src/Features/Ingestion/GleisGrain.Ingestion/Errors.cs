using GleisGrain.Common.Errors;
using GleisGrain.Ingestion.Contracts;

namespace GleisGrain.Ingestion;

internal sealed record SourceNotFound(SourceId SourceId) : DomainError(
    "ingestion.source.not_found",
    "Source not found",
    StatusCodes.Status404NotFound,
    $"Source '{SourceId}' does not exist.");
