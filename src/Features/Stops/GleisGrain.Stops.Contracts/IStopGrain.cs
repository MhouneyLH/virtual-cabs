namespace GleisGrain.Stops.Contracts;

/// <remarks>The grain key is the <see cref="StopId"/> value.</remarks>
[Alias("GleisGrain.Stops.Contracts.IStopGrain")]
public interface IStopGrain : IGrainWithStringKey
{
    [Alias("GetAsync")]
    Task<StopSnapshot?> GetAsync();
}
