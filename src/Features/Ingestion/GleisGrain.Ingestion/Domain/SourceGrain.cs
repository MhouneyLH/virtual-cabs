using GleisGrain.Ingestion.Contracts;

namespace GleisGrain.Ingestion.Domain;

internal sealed class SourceGrain(
    [PersistentState("source", IngestionOptions.StorageName)] IPersistentState<SourceState> state)
    : Grain, ISourceGrain
{
    public Task<SourceSnapshot?> GetAsync() =>
        Task.FromResult<SourceSnapshot?>(state.RecordExists ? new Source(SourceId.From(this.GetPrimaryKeyString()), state.State).ToSnapshot() : null);
}
