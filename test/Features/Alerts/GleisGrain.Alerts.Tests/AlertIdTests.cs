using System.Text.Json;
using GleisGrain.Alerts.Contracts;

namespace GleisGrain.Alerts.Tests;

public class AlertIdTests
{
    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData("not valid!")]
    public void TryParse_InvalidValue_ReturnsFalse(string value) =>
        Assert.False(AlertId.TryParse(value, null, out _));

    [Fact]
    public void Json_RoundTripsAsPlainString()
    {
        var id = AlertId.From("8f3a1c");

        var json = JsonSerializer.Serialize(id);

        Assert.Equal("\"8f3a1c\"", json);
        Assert.Equal(id, JsonSerializer.Deserialize<AlertId>(json));
    }
}
