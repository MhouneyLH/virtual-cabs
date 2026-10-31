using System.Text.Json;
using GleisGrain.Stops.Contracts;

namespace GleisGrain.Stops.Tests;

public class StopIdTests
{
    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData("not valid!")]
    public void TryParse_InvalidValue_ReturnsFalse(string value) =>
        Assert.False(StopId.TryParse(value, null, out _));

    [Fact]
    public void Json_RoundTripsAsPlainString()
    {
        var id = StopId.From("8f3a1c");

        var json = JsonSerializer.Serialize(id);

        Assert.Equal("\"8f3a1c\"", json);
        Assert.Equal(id, JsonSerializer.Deserialize<StopId>(json));
    }
}
