using System.ComponentModel;
using GleisGrain.Common.Errors;
using GleisGrain.Ingestion.Contracts;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace GleisGrain.Ingestion.Endpoints;

internal sealed record GetSourceRequest
{
    [FromRoute(Name = "id")]
    [Description("Identifier of the source.")]
    public required SourceId Id { get; init; }
}

internal sealed record GetSourceResponse
{
    [Description("Identifier of the source.")]
    public required SourceId Id { get; init; }

    [Description("When the source was created.")]
    public required DateTimeOffset CreatedAt { get; init; }
}

internal sealed class GetSource
{
    public static void Map(IEndpointRouteBuilder routes) =>
        routes.MapGet("/sources/{id}", Handle)
            .WithName("GetSource")
            .WithSummary("Gets a source by id.")
            .WithDescription("Part of the Ingestion context: EFA XML client, parsing and rate limiting.")
            .ProducesProblem(StatusCodes.Status404NotFound);

    private static async Task<Ok<GetSourceResponse>> Handle(
        [AsParameters] GetSourceRequest request,
        IGrainFactory grains,
        IngestionMetrics metrics,
        ILoggerFactory loggerFactory)
    {
        loggerFactory.CreateLogger<GetSource>().LookingUpSource(request.Id);

        var snapshot = await grains.GetGrain<ISourceGrain>(request.Id.Value).GetAsync();
        metrics.SourceLookedUp(snapshot is not null);

        if (snapshot is null)
        {
            throw new DomainException(new SourceNotFound(request.Id));
        }

        return TypedResults.Ok(new GetSourceResponse { Id = snapshot.Id, CreatedAt = snapshot.CreatedAt });
    }
}
