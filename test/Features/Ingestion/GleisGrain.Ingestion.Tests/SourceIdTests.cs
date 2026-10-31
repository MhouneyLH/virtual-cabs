using System.Text.Json;
using GleisGrain.Ingestion.Contracts;

namespace GleisGrain.Ingestion.Tests;

public class SourceIdTests
{
    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData("not valid!")]
    public void TryParse_InvalidValue_ReturnsFalse(string value) =>
        Assert.False(SourceId.TryParse(value, null, out _));

    [Fact]
    public void Json_RoundTripsAsPlainString()
    {
        var id = SourceId.From("8f3a1c");

        var json = JsonSerializer.Serialize(id);

        Assert.Equal("\"8f3a1c\"", json);
        Assert.Equal(id, JsonSerializer.Deserialize<SourceId>(json));
    }
}
