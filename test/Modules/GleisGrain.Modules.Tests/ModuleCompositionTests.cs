using System.Net;
using System.Net.Sockets;
using GleisGrain.Alerts;
using GleisGrain.Alerts.Contracts;
using GleisGrain.Ingestion;
using GleisGrain.Ingestion.Contracts;
using GleisGrain.Stops;
using GleisGrain.Stops.Contracts;
using GleisGrain.Trips;
using GleisGrain.Trips.Contracts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace GleisGrain.Modules.Tests;

/// <summary>
/// Composes all modules into one silo like the host does, with in-memory storage and
/// localhost clustering, and checks that every module's grains are registered and reachable.
/// </summary>
public sealed class ModuleCompositionTests : IAsyncLifetime
{
    private IHost? _host;

    public async ValueTask InitializeAsync()
    {
        var builder = Host.CreateApplicationBuilder();

        builder
            .AddStopsModule()
            .AddTripsModule()
            .AddAlertsModule()
            .AddIngestionModule();

        builder.UseOrleans(silo =>
        {
            silo.UseLocalhostClustering(siloPort: FreePort(), gatewayPort: FreePort());
            silo.AddMemoryGrainStorage(StopsModule.StorageName);
            silo.AddMemoryGrainStorage(TripsModule.StorageName);
            silo.AddMemoryGrainStorage(AlertsModule.StorageName);
            silo.AddMemoryGrainStorage(IngestionModule.StorageName);
        });

        _host = builder.Build();
        await _host.StartAsync();
    }

    public async ValueTask DisposeAsync()
    {
        if (_host is not null)
        {
            await _host.StopAsync();
            _host.Dispose();
        }
    }

    [Fact]
    public async Task EveryModulesGrain_IsRegisteredInTheSharedSilo()
    {
        var grains = _host!.Services.GetRequiredService<IGrainFactory>();

        var stop = await grains.GetGrain<IStopGrain>(StopId.From("linz-hbf").Value).GetAsync();
        var trip = await grains.GetGrain<ITripGrain>(TripId.From("trip-1").Value).GetAsync();
        var alert = await grains.GetGrain<IAlertGrain>(AlertId.From("alert-1").Value).GetAsync();
        var source = await grains.GetGrain<ISourceGrain>(SourceId.From("linz-ag").Value).GetAsync();

        Assert.Null(stop);
        Assert.Null(trip);
        Assert.Null(alert);
        Assert.Null(source);
    }

    private static int FreePort()
    {
        using var listener = new TcpListener(IPAddress.Loopback, 0);
        listener.Start();
        return ((IPEndPoint)listener.LocalEndpoint).Port;
    }
}
