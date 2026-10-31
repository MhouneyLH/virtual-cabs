using System.ComponentModel.DataAnnotations;

namespace GleisGrain.Alerts;

internal sealed class AlertsOptions
{
    public const string SectionName = "Alerts";

    /// <summary>Grain storage name; equals the connection string name of the alerts database.</summary>
    public const string StorageName = "alerts";

    [Range(typeof(TimeSpan), "00:00:01", "1.00:00:00")]
    public TimeSpan ReminderPeriod { get; init; } = TimeSpan.FromMinutes(1);
}
