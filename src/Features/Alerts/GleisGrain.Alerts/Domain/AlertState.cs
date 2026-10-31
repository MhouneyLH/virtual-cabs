namespace GleisGrain.Alerts.Domain;

/// <summary>Persisted blob. Only add fields with new ids; bump <see cref="CurrentSchemaVersion"/> and migrate on activation.</summary>
[GenerateSerializer, Alias("GleisGrain.Alerts.Domain.AlertState")]
internal sealed class AlertState
{
    public const int CurrentSchemaVersion = 1;

    [Id(0)]
    public int SchemaVersion { get; set; } = CurrentSchemaVersion;

    [Id(1)]
    public DateTimeOffset CreatedAt { get; set; }
}
