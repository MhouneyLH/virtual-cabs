namespace GleisGrain.Trips.Contracts;

[GenerateSerializer, Immutable, Alias("GleisGrain.Trips.Contracts.TripSnapshot")]
public sealed record TripSnapshot([property: Id(0)] TripId Id, [property: Id(1)] DateTimeOffset CreatedAt);
