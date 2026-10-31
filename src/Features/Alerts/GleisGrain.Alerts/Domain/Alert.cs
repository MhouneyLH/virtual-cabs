using GleisGrain.Alerts.Contracts;

namespace GleisGrain.Alerts.Domain;

/// <summary>Pure domain model: no Orleans runtime, no I/O. The grain loads state, calls this, persists.</summary>
internal sealed class Alert(AlertId id, AlertState state)
{
    public AlertId Id { get; } = id;

    public AlertSnapshot ToSnapshot() => new(Id, state.CreatedAt);
}
