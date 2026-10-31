namespace GleisGrain.Alerts.Contracts;

/// <remarks>The grain key is the <see cref="AlertId"/> value.</remarks>
[Alias("GleisGrain.Alerts.Contracts.IAlertGrain")]
public interface IAlertGrain : IGrainWithStringKey
{
    [Alias("GetAsync")]
    Task<AlertSnapshot?> GetAsync();
}
