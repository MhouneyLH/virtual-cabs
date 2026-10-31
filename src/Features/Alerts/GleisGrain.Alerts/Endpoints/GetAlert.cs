using System.ComponentModel;
using GleisGrain.Alerts.Contracts;
using GleisGrain.Common.Errors;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace GleisGrain.Alerts.Endpoints;

internal sealed record GetAlertRequest
{
    [FromRoute(Name = "id")]
    [Description("Identifier of the alert.")]
    public required AlertId Id { get; init; }
}

internal sealed record GetAlertResponse
{
    [Description("Identifier of the alert.")]
    public required AlertId Id { get; init; }

    [Description("When the alert was created.")]
    public required DateTimeOffset CreatedAt { get; init; }
}

internal sealed class GetAlert
{
    public static void Map(IEndpointRouteBuilder routes) =>
        routes.MapGet("/alerts/{id}", Handle)
            .WithName("GetAlert")
            .WithSummary("Gets a alert by id.")
            .WithDescription("Part of the Alerts context: service notices and disruptions.")
            .ProducesProblem(StatusCodes.Status404NotFound);

    private static async Task<Ok<GetAlertResponse>> Handle(
        [AsParameters] GetAlertRequest request,
        IGrainFactory grains,
        AlertsMetrics metrics,
        ILoggerFactory loggerFactory)
    {
        loggerFactory.CreateLogger<GetAlert>().LookingUpAlert(request.Id);

        var snapshot = await grains.GetGrain<IAlertGrain>(request.Id.Value).GetAsync();
        metrics.AlertLookedUp(snapshot is not null);

        if (snapshot is null)
        {
            throw new DomainException(new AlertNotFound(request.Id));
        }

        return TypedResults.Ok(new GetAlertResponse { Id = snapshot.Id, CreatedAt = snapshot.CreatedAt });
    }
}
