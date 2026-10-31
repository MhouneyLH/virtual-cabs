using GleisGrain.Stops.Contracts;

namespace GleisGrain.Stops;

internal static partial class StopsLogs
{
    [LoggerMessage(EventId = 1000, Level = LogLevel.Debug, Message = "Looking up stop {StopId}")]
    public static partial void LookingUpStop(this ILogger logger, StopId stopId);
}
