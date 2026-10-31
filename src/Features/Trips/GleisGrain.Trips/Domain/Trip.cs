using GleisGrain.Trips.Contracts;

namespace GleisGrain.Trips.Domain;

/// <summary>Pure domain model: no Orleans runtime, no I/O. The grain loads state, calls this, persists.</summary>
internal sealed class Trip(TripId id, TripState state)
{
    public TripId Id { get; } = id;

    public TripSnapshot ToSnapshot() => new(Id, state.CreatedAt);
}
