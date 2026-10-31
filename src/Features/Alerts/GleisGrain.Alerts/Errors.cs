using GleisGrain.Alerts.Contracts;
using GleisGrain.Common.Errors;

namespace GleisGrain.Alerts;

internal sealed record AlertNotFound(AlertId AlertId) : DomainError(
    "alerts.alert.not_found",
    "Alert not found",
    StatusCodes.Status404NotFound,
    $"Alert '{AlertId}' does not exist.");
