namespace GleisGrain.Ingestion.Contracts;

[GenerateSerializer, Immutable, Alias("GleisGrain.Ingestion.Contracts.SourceSnapshot")]
public sealed record SourceSnapshot([property: Id(0)] SourceId Id, [property: Id(1)] DateTimeOffset CreatedAt);
