using GleisGrain.Common;
using GleisGrain.Common.Options;
using GleisGrain.Ingestion;
using GleisGrain.Ingestion.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.AddGleisGrainObservability("gleisgrain-ingestion", IngestionMetrics.MeterName);
builder.AddGleisGrainHttp();
builder.AddGleisGrainOrleans(IngestionOptions.StorageName);

builder.Services.AddValidatedOptions<IngestionOptions>(IngestionOptions.SectionName);
builder.Services.AddSingleton<IngestionMetrics>();

var app = builder.Build();

app.UseGleisGrainHttp();

var api = app.MapGroup("/ingestion");
GetSource.Map(api);

await app.RunAsync();
