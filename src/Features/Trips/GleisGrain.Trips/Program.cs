using GleisGrain.Common;
using GleisGrain.Common.Options;
using GleisGrain.Trips;
using GleisGrain.Trips.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.AddGleisGrainObservability("gleisgrain-trips", TripsMetrics.MeterName);
builder.AddGleisGrainHttp();
builder.AddGleisGrainOrleans(TripsOptions.StorageName);

builder.Services.AddValidatedOptions<TripsOptions>(TripsOptions.SectionName);
builder.Services.AddSingleton<TripsMetrics>();

var app = builder.Build();

app.UseGleisGrainHttp();

var api = app.MapGroup("/trips");
GetTrip.Map(api);

await app.RunAsync();
