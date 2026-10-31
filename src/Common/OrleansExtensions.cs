using GleisGrain.Common.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Orleans.Configuration;

namespace GleisGrain.Common;

public static class OrleansExtensions
{
    private const string PostgresInvariant = "Npgsql";

    /// <summary>
    /// Configures one silo of the shared gleis-grain cluster.
    /// Membership and reminders live in the shared <c>orleans</c> database, grain state in the feature's own database.
    /// </summary>
    /// <param name="builder">The host builder to configure.</param>
    /// <param name="storageName">Grain storage name; by convention equal to the feature's connection string name.</param>
    public static TBuilder AddGleisGrainOrleans<TBuilder>(this TBuilder builder, string storageName)
        where TBuilder : IHostApplicationBuilder
    {
        builder.Services.AddValidatedOptions<OrleansOptions>(OrleansOptions.SectionName);

        var options = builder.Configuration.GetSection(OrleansOptions.SectionName).Get<OrleansOptions>() ?? new OrleansOptions();
        var clusteringConnection = builder.Configuration.GetConnectionString(options.ClusteringConnectionName)
            ?? throw new InvalidOperationException($"Connection string '{options.ClusteringConnectionName}' is missing.");
        var storageConnection = builder.Configuration.GetConnectionString(storageName)
            ?? throw new InvalidOperationException($"Connection string '{storageName}' is missing.");

        builder.UseOrleans(silo =>
        {
            silo.Configure<ClusterOptions>(cluster =>
            {
                cluster.ClusterId = options.ClusterId;
                cluster.ServiceId = options.ServiceId;
            });

            silo.ConfigureEndpoints(siloPort: options.SiloPort, gatewayPort: options.GatewayPort);

            silo.UseAdoNetClustering(o =>
            {
                o.Invariant = PostgresInvariant;
                o.ConnectionString = clusteringConnection;
            });

            silo.UseAdoNetReminderService(o =>
            {
                o.Invariant = PostgresInvariant;
                o.ConnectionString = clusteringConnection;
            });

            silo.AddAdoNetGrainStorage(storageName, o =>
            {
                o.Invariant = PostgresInvariant;
                o.ConnectionString = storageConnection;
            });
        });

        return builder;
    }
}
