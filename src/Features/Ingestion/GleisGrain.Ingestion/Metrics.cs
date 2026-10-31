using System.Diagnostics.Metrics;

namespace GleisGrain.Ingestion;

internal sealed class IngestionMetrics
{
    public const string MeterName = "GleisGrain.Ingestion";

    private readonly Counter<long> _sourceLookups;

    public IngestionMetrics(IMeterFactory meterFactory)
    {
        var meter = meterFactory.Create(MeterName);
        _sourceLookups = meter.CreateCounter<long>(
            "gleisgrain.ingestion.source.lookups",
            unit: "{lookup}",
            description: "Number of Source lookups, tagged by whether the source was found.");
    }

    public void SourceLookedUp(bool found) =>
        _sourceLookups.Add(1, new KeyValuePair<string, object?>("found", found));
}
