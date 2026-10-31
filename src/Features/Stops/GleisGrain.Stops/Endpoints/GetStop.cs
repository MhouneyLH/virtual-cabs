using System.ComponentModel;
using GleisGrain.Common.Errors;
using GleisGrain.Stops.Contracts;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace GleisGrain.Stops.Endpoints;

internal sealed record GetStopRequest
{
    [FromRoute(Name = "id")]
    [Description("Identifier of the stop.")]
    public required StopId Id { get; init; }
}

internal sealed record GetStopResponse
{
    [Description("Identifier of the stop.")]
    public required StopId Id { get; init; }

    [Description("When the stop was created.")]
    public required DateTimeOffset CreatedAt { get; init; }
}

internal sealed class GetStop
{
    public static void Map(IEndpointRouteBuilder routes) =>
        routes.MapGet("/stops/{id}", Handle)
            .WithName("GetStop")
            .WithSummary("Gets a stop by id.")
            .WithDescription("Part of the Stops context: stops and their departure boards.")
            .ProducesProblem(StatusCodes.Status404NotFound);

    private static async Task<Ok<GetStopResponse>> Handle(
        [AsParameters] GetStopRequest request,
        IGrainFactory grains,
        StopsMetrics metrics,
        ILoggerFactory loggerFactory)
    {
        loggerFactory.CreateLogger<GetStop>().LookingUpStop(request.Id);

        var snapshot = await grains.GetGrain<IStopGrain>(request.Id.Value).GetAsync();
        metrics.StopLookedUp(snapshot is not null);

        if (snapshot is null)
        {
            throw new DomainException(new StopNotFound(request.Id));
        }

        return TypedResults.Ok(new GetStopResponse { Id = snapshot.Id, CreatedAt = snapshot.CreatedAt });
    }
}
