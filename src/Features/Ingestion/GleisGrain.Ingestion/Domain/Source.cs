using GleisGrain.Ingestion.Contracts;

namespace GleisGrain.Ingestion.Domain;

/// <summary>Pure domain model: no Orleans runtime, no I/O. The grain loads state, calls this, persists.</summary>
internal sealed class Source(SourceId id, SourceState state)
{
    public SourceId Id { get; } = id;

    public SourceSnapshot ToSnapshot() => new(Id, state.CreatedAt);
}
