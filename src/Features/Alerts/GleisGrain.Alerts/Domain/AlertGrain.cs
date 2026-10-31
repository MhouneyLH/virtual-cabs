using GleisGrain.Alerts.Contracts;

namespace GleisGrain.Alerts.Domain;

internal sealed class AlertGrain(
    [PersistentState("alert", AlertsOptions.StorageName)] IPersistentState<AlertState> state)
    : Grain, IAlertGrain
{
    public Task<AlertSnapshot?> GetAsync() =>
        Task.FromResult<AlertSnapshot?>(state.RecordExists ? new Alert(AlertId.From(this.GetPrimaryKeyString()), state.State).ToSnapshot() : null);
}
