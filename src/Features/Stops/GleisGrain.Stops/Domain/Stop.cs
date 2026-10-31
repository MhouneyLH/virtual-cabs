using GleisGrain.Stops.Contracts;

namespace GleisGrain.Stops.Domain;

/// <summary>Pure domain model: no Orleans runtime, no I/O. The grain loads state, calls this, persists.</summary>
internal sealed class Stop(StopId id, StopState state)
{
    public StopId Id { get; } = id;

    public StopSnapshot ToSnapshot() => new(Id, state.CreatedAt);
}
