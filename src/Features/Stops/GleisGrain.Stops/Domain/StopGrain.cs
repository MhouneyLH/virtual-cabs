using GleisGrain.Stops.Contracts;

namespace GleisGrain.Stops.Domain;

internal sealed class StopGrain(
    [PersistentState("stop", StopsOptions.StorageName)] IPersistentState<StopState> state)
    : Grain, IStopGrain
{
    public Task<StopSnapshot?> GetAsync() =>
        Task.FromResult<StopSnapshot?>(state.RecordExists ? new Stop(StopId.From(this.GetPrimaryKeyString()), state.State).ToSnapshot() : null);
}
