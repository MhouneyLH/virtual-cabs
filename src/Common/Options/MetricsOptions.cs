using System.ComponentModel.DataAnnotations;

namespace GleisGrain.Common.Options;

public sealed class MetricsOptions
{
    public const string SectionName = "Metrics";

    [Range(1_000, 300_000)]
    public int ExportIntervalMilliseconds { get; init; } = 10_000;

    public bool IncludeOrleansMeters { get; init; } = true;
}
