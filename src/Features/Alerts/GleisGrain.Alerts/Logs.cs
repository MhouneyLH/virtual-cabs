using GleisGrain.Alerts.Contracts;

namespace GleisGrain.Alerts;

internal static partial class AlertsLogs
{
    [LoggerMessage(EventId = 3000, Level = LogLevel.Debug, Message = "Looking up alert {AlertId}")]
    public static partial void LookingUpAlert(this ILogger logger, AlertId alertId);
}
