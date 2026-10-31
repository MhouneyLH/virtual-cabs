using GleisGrain.Trips.Contracts;

namespace GleisGrain.Trips.Domain;

internal sealed class TripGrain(
    [PersistentState("trip", TripsOptions.StorageName)] IPersistentState<TripState> state)
    : Grain, ITripGrain
{
    public Task<TripSnapshot?> GetAsync() =>
        Task.FromResult<TripSnapshot?>(state.RecordExists ? new Trip(TripId.From(this.GetPrimaryKeyString()), state.State).ToSnapshot() : null);
}
