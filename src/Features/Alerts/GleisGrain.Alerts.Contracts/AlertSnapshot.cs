namespace GleisGrain.Alerts.Contracts;

[GenerateSerializer, Immutable, Alias("GleisGrain.Alerts.Contracts.AlertSnapshot")]
public sealed record AlertSnapshot([property: Id(0)] AlertId Id, [property: Id(1)] DateTimeOffset CreatedAt);
