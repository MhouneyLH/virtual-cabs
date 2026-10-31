using System.ComponentModel.DataAnnotations;

namespace GleisGrain.Ingestion;

internal sealed class IngestionOptions
{
    public const string SectionName = "Ingestion";

    /// <summary>Grain storage name; equals the connection string name of the ingestion database.</summary>
    public const string StorageName = "ingestion";

    [Range(typeof(TimeSpan), "00:00:01", "1.00:00:00")]
    public TimeSpan ReminderPeriod { get; init; } = TimeSpan.FromMinutes(1);
}
