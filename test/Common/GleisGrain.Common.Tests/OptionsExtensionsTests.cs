using GleisGrain.Common.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace GleisGrain.Common.Tests;

public class OptionsExtensionsTests
{
    [Fact]
    public void AddValidatedOptions_InvalidValue_ThrowsOnAccess()
    {
        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?> { ["Http:RequestTimeoutSeconds"] = "0" })
            .Build();
        var services = new ServiceCollection()
            .AddSingleton<IConfiguration>(configuration)
            .AddValidatedOptions<HttpOptions>(HttpOptions.SectionName)
            .BuildServiceProvider();

        Assert.Throws<OptionsValidationException>(() => services.GetRequiredService<IOptions<HttpOptions>>().Value);
    }

    [Fact]
    public void AddValidatedOptions_ValidValue_Binds()
    {
        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?> { ["Http:RequestTimeoutSeconds"] = "45" })
            .Build();
        var services = new ServiceCollection()
            .AddSingleton<IConfiguration>(configuration)
            .AddValidatedOptions<HttpOptions>(HttpOptions.SectionName)
            .BuildServiceProvider();

        Assert.Equal(45, services.GetRequiredService<IOptions<HttpOptions>>().Value.RequestTimeoutSeconds);
    }
}
