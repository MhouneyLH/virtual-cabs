using System.Diagnostics.Metrics;

namespace GleisGrain.Stops;

internal sealed class StopsMetrics
{
    public const string MeterName = "GleisGrain.Stops";

    private readonly Counter<long> _stopLookups;

    public StopsMetrics(IMeterFactory meterFactory)
    {
        var meter = meterFactory.Create(MeterName);
        _stopLookups = meter.CreateCounter<long>(
            "gleisgrain.stops.stop.lookups",
            unit: "{lookup}",
            description: "Number of Stop lookups, tagged by whether the stop was found.");
    }

    public void StopLookedUp(bool found) =>
        _stopLookups.Add(1, new KeyValuePair<string, object?>("found", found));
}
