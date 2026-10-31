using GleisGrain.Trips.Contracts;

namespace GleisGrain.Trips;

internal static partial class TripsLogs
{
    [LoggerMessage(EventId = 2000, Level = LogLevel.Debug, Message = "Looking up trip {TripId}")]
    public static partial void LookingUpTrip(this ILogger logger, TripId tripId);
}
