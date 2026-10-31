using System.Diagnostics.Metrics;

namespace GleisGrain.Alerts;

internal sealed class AlertsMetrics
{
    public const string MeterName = "GleisGrain.Alerts";

    private readonly Counter<long> _alertLookups;

    public AlertsMetrics(IMeterFactory meterFactory)
    {
        var meter = meterFactory.Create(MeterName);
        _alertLookups = meter.CreateCounter<long>(
            "gleisgrain.alerts.alert.lookups",
            unit: "{lookup}",
            description: "Number of Alert lookups, tagged by whether the alert was found.");
    }

    public void AlertLookedUp(bool found) =>
        _alertLookups.Add(1, new KeyValuePair<string, object?>("found", found));
}
