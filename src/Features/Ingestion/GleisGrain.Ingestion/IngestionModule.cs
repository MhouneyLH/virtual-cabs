using GleisGrain.Common.Options;
using GleisGrain.Ingestion.Endpoints;

namespace GleisGrain.Ingestion;

/// <summary>
/// Public surface of the Ingestion module. Everything else in this project is internal.
/// The host calls <see cref="AddIngestionModule{TBuilder}"/> and <see cref="MapIngestionEndpoints"/>.
/// </summary>
public static class IngestionModule
{
    /// <summary>Grain storage name, equals the connection string name of the ingestion database.</summary>
    public const string StorageName = IngestionOptions.StorageName;

    /// <summary>Name of the meter the module publishes metrics on.</summary>
    public const string MeterName = IngestionMetrics.MeterName;

    public static TBuilder AddIngestionModule<TBuilder>(this TBuilder builder)
        where TBuilder : IHostApplicationBuilder
    {
        builder.Services.AddValidatedOptions<IngestionOptions>(IngestionOptions.SectionName);
        builder.Services.AddSingleton<IngestionMetrics>();

        return builder;
    }

    public static IEndpointRouteBuilder MapIngestionEndpoints(this IEndpointRouteBuilder routes)
    {
        var api = routes.MapGroup("/ingestion");
        GetSource.Map(api);

        return routes;
    }
}
