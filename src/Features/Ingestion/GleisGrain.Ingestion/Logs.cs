using GleisGrain.Ingestion.Contracts;

namespace GleisGrain.Ingestion;

internal static partial class IngestionLogs
{
    [LoggerMessage(EventId = 4000, Level = LogLevel.Debug, Message = "Looking up source {SourceId}")]
    public static partial void LookingUpSource(this ILogger logger, SourceId sourceId);
}
