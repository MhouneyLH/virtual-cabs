using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace GleisGrain.Common.Abstractions.Tests;

public class StronglyTypedIdJsonConverterTests
{
    [JsonConverter(typeof(StronglyTypedIdJsonConverter<TestId>))]
    private readonly record struct TestId(int Value) : IStronglyTypedId<TestId, int>
    {
        public static TestId Parse(string s, IFormatProvider? provider) => new(int.Parse(s, provider));

        public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, out TestId result)
        {
            var ok = int.TryParse(s, provider, out var value);
            result = new TestId(value);
            return ok;
        }

        public override string ToString() => Value.ToString(System.Globalization.CultureInfo.InvariantCulture);
    }

    [Fact]
    public void RoundTrip_WritesPlainString()
    {
        var json = JsonSerializer.Serialize(new TestId(7));

        Assert.Equal("\"7\"", json);
        Assert.Equal(new TestId(7), JsonSerializer.Deserialize<TestId>(json));
    }

    [Fact]
    public void Deserialize_InvalidValue_ThrowsJsonException() =>
        Assert.Throws<JsonException>(() => JsonSerializer.Deserialize<TestId>("\"abc\""));

    [Fact]
    public void RoundTrip_WorksAsDictionaryKey()
    {
        var json = JsonSerializer.Serialize(new Dictionary<TestId, string> { [new TestId(1)] = "a" });

        Assert.Equal("{\"1\":\"a\"}", json);
        Assert.Equal("a", JsonSerializer.Deserialize<Dictionary<TestId, string>>(json)![new TestId(1)]);
    }
}
