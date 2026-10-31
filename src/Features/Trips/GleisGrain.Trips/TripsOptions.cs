using System.ComponentModel.DataAnnotations;

namespace GleisGrain.Trips;

internal sealed class TripsOptions
{
    public const string SectionName = "Trips";

    /// <summary>Grain storage name; equals the connection string name of the trips database.</summary>
    public const string StorageName = "trips";

    [Range(typeof(TimeSpan), "00:00:01", "1.00:00:00")]
    public TimeSpan ReminderPeriod { get; init; } = TimeSpan.FromMinutes(1);
}
