using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace GleisGrain.Common.Abstractions;

/// <summary>Writes an id as a plain JSON string and reads it back through <see cref="IParsable{TSelf}"/>.</summary>
public sealed class StronglyTypedIdJsonConverter<TId> : JsonConverter<TId>
    where TId : struct, IParsable<TId>
{
    public override TId Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) =>
        Parse(reader.GetString());

    public override void Write(Utf8JsonWriter writer, TId value, JsonSerializerOptions options) =>
        writer.WriteStringValue(value.ToString());

    public override TId ReadAsPropertyName(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) =>
        Parse(reader.GetString());

    public override void WriteAsPropertyName(Utf8JsonWriter writer, TId value, JsonSerializerOptions options) =>
        writer.WritePropertyName(value.ToString() ?? string.Empty);

    private static TId Parse(string? text) =>
        TId.TryParse(text, CultureInfo.InvariantCulture, out var id)
            ? id
            : throw new JsonException($"'{text}' is not a valid {typeof(TId).Name}.");
}
