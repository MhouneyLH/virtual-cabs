namespace GleisGrain.Trips.Contracts;

/// <remarks>The grain key is the <see cref="TripId"/> value.</remarks>
[Alias("GleisGrain.Trips.Contracts.ITripGrain")]
public interface ITripGrain : IGrainWithStringKey
{
    [Alias("GetAsync")]
    Task<TripSnapshot?> GetAsync();
}
