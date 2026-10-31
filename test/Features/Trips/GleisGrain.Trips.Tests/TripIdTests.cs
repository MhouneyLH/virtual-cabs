using System.Text.Json;
using GleisGrain.Trips.Contracts;

namespace GleisGrain.Trips.Tests;

public class TripIdTests
{
    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData("not valid!")]
    public void TryParse_InvalidValue_ReturnsFalse(string value) =>
        Assert.False(TripId.TryParse(value, null, out _));

    [Fact]
    public void Json_RoundTripsAsPlainString()
    {
        var id = TripId.From("8f3a1c");

        var json = JsonSerializer.Serialize(id);

        Assert.Equal("\"8f3a1c\"", json);
        Assert.Equal(id, JsonSerializer.Deserialize<TripId>(json));
    }
}
