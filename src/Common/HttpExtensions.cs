using GleisGrain.Common.Errors;
using GleisGrain.Common.Ids;
using GleisGrain.Common.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Scalar.AspNetCore;
using HttpOptions = GleisGrain.Common.Options.HttpOptions;

namespace GleisGrain.Common;

public static class HttpExtensions
{
    public static WebApplicationBuilder AddGleisGrainHttp(this WebApplicationBuilder builder)
    {
        builder.Services.AddValidatedOptions<HttpOptions>(HttpOptions.SectionName);

        builder.Services.AddProblemDetails();
        builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
        builder.Services.AddValidation();
        builder.Services.AddOpenApi(options => options.AddSchemaTransformer<StronglyTypedIdSchemaTransformer>());
        builder.Services.AddRequestTimeouts();
        builder.Services
            .AddOptions<Microsoft.AspNetCore.Http.Timeouts.RequestTimeoutOptions>()
            .Configure<IOptions<HttpOptions>>((timeouts, http) =>
                timeouts.DefaultPolicy = new Microsoft.AspNetCore.Http.Timeouts.RequestTimeoutPolicy
                {
                    Timeout = TimeSpan.FromSeconds(http.Value.RequestTimeoutSeconds),
                });

        return builder;
    }

    public static WebApplication UseGleisGrainHttp(this WebApplication app)
    {
        app.UseExceptionHandler();
        app.UseStatusCodePages();
        app.UseRequestTimeouts();

        var http = app.Services.GetRequiredService<IOptions<HttpOptions>>().Value;
        app.MapOpenApi();
        if (app.Environment.IsDevelopment() || http.EnableApiReference)
        {
            app.MapScalarApiReference();
        }

        return app;
    }
}
