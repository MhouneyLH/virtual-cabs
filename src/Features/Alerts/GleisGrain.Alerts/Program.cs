using GleisGrain.Alerts;
using GleisGrain.Alerts.Endpoints;
using GleisGrain.Common;
using GleisGrain.Common.Options;

var builder = WebApplication.CreateBuilder(args);

builder.AddGleisGrainObservability("gleisgrain-alerts", AlertsMetrics.MeterName);
builder.AddGleisGrainHttp();
builder.AddGleisGrainOrleans(AlertsOptions.StorageName);

builder.Services.AddValidatedOptions<AlertsOptions>(AlertsOptions.SectionName);
builder.Services.AddSingleton<AlertsMetrics>();

var app = builder.Build();

app.UseGleisGrainHttp();

var api = app.MapGroup("/alerts");
GetAlert.Map(api);

await app.RunAsync();
