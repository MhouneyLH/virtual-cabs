namespace GleisGrain.Stops.Contracts;

[GenerateSerializer, Immutable, Alias("GleisGrain.Stops.Contracts.StopSnapshot")]
public sealed record StopSnapshot([property: Id(0)] StopId Id, [property: Id(1)] DateTimeOffset CreatedAt);
