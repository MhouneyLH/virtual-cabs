using GleisGrain.Common;
using GleisGrain.Common.Options;
using GleisGrain.Stops;
using GleisGrain.Stops.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.AddGleisGrainObservability("gleisgrain-stops", StopsMetrics.MeterName);
builder.AddGleisGrainHttp();
builder.AddGleisGrainOrleans(StopsOptions.StorageName);

builder.Services.AddValidatedOptions<StopsOptions>(StopsOptions.SectionName);
builder.Services.AddSingleton<StopsMetrics>();

var app = builder.Build();

app.UseGleisGrainHttp();

var api = app.MapGroup("/stops");
GetStop.Map(api);

await app.RunAsync();
