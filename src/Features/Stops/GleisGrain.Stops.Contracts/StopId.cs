using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using GleisGrain.Common.Abstractions;

namespace GleisGrain.Stops.Contracts;

[GenerateSerializer, Immutable, Alias("GleisGrain.Stops.Contracts.StopId")]
[JsonConverter(typeof(StronglyTypedIdJsonConverter<StopId>))]
public readonly record struct StopId([property: Id(0)] string Value) : IStronglyTypedId<StopId, string>
{
    public const int MaxLength = 64;

    public static StopId From(string value) =>
        TryParse(value, null, out var id) ? id : throw new ArgumentException($"'{value}' is not a valid Stop id.", nameof(value));

    public static StopId Parse(string s, IFormatProvider? provider) =>
        TryParse(s, provider, out var id) ? id : throw new FormatException($"'{s}' is not a valid Stop id.");

    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, out StopId result)
    {
        if (!string.IsNullOrWhiteSpace(s) && s.Length <= MaxLength && s.All(c => char.IsAsciiLetterOrDigit(c) || c is ':' or '_' or '-' or '.'))
        {
            result = new StopId(s);
            return true;
        }

        result = default;
        return false;
    }

    public override string ToString() => Value ?? string.Empty;
}
