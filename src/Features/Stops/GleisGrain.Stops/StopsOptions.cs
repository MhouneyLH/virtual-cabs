using System.ComponentModel.DataAnnotations;

namespace GleisGrain.Stops;

internal sealed class StopsOptions
{
    public const string SectionName = "Stops";

    /// <summary>Grain storage name; equals the connection string name of the stops database.</summary>
    public const string StorageName = "stops";

    [Range(typeof(TimeSpan), "00:00:01", "1.00:00:00")]
    public TimeSpan ReminderPeriod { get; init; } = TimeSpan.FromMinutes(1);
}
