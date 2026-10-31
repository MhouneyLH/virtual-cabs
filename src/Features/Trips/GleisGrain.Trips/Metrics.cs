using System.Diagnostics.Metrics;

namespace GleisGrain.Trips;

internal sealed class TripsMetrics
{
    public const string MeterName = "GleisGrain.Trips";

    private readonly Counter<long> _tripLookups;

    public TripsMetrics(IMeterFactory meterFactory)
    {
        var meter = meterFactory.Create(MeterName);
        _tripLookups = meter.CreateCounter<long>(
            "gleisgrain.trips.trip.lookups",
            unit: "{lookup}",
            description: "Number of Trip lookups, tagged by whether the trip was found.");
    }

    public void TripLookedUp(bool found) =>
        _tripLookups.Add(1, new KeyValuePair<string, object?>("found", found));
}
