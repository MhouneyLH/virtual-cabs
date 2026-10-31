using System.ComponentModel;
using GleisGrain.Common.Errors;
using GleisGrain.Trips.Contracts;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace GleisGrain.Trips.Endpoints;

internal sealed record GetTripRequest
{
    [FromRoute(Name = "id")]
    [Description("Identifier of the trip.")]
    public required TripId Id { get; init; }
}

internal sealed record GetTripResponse
{
    [Description("Identifier of the trip.")]
    public required TripId Id { get; init; }

    [Description("When the trip was created.")]
    public required DateTimeOffset CreatedAt { get; init; }
}

internal sealed class GetTrip
{
    public static void Map(IEndpointRouteBuilder routes) =>
        routes.MapGet("/trips/{id}", Handle)
            .WithName("GetTrip")
            .WithSummary("Gets a trip by id.")
            .WithDescription("Part of the Trips context: trips with delay state.")
            .ProducesProblem(StatusCodes.Status404NotFound);

    private static async Task<Ok<GetTripResponse>> Handle(
        [AsParameters] GetTripRequest request,
        IGrainFactory grains,
        TripsMetrics metrics,
        ILoggerFactory loggerFactory)
    {
        loggerFactory.CreateLogger<GetTrip>().LookingUpTrip(request.Id);

        var snapshot = await grains.GetGrain<ITripGrain>(request.Id.Value).GetAsync();
        metrics.TripLookedUp(snapshot is not null);

        if (snapshot is null)
        {
            throw new DomainException(new TripNotFound(request.Id));
        }

        return TypedResults.Ok(new GetTripResponse { Id = snapshot.Id, CreatedAt = snapshot.CreatedAt });
    }
}
