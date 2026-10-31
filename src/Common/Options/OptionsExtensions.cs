using Microsoft.Extensions.DependencyInjection;

namespace GleisGrain.Common.Options;

public static class OptionsExtensions
{
    /// <summary>Binds <typeparamref name="TOptions"/> to a section and fails startup when data annotations are violated.</summary>
    public static IServiceCollection AddValidatedOptions<TOptions>(this IServiceCollection services, string sectionName)
        where TOptions : class
    {
        services
            .AddOptions<TOptions>()
            .BindConfiguration(sectionName)
            .ValidateDataAnnotations()
            .ValidateOnStart();

        return services;
    }
}
