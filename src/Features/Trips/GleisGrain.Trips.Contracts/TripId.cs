using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using GleisGrain.Common.Abstractions;

namespace GleisGrain.Trips.Contracts;

[GenerateSerializer, Immutable, Alias("GleisGrain.Trips.Contracts.TripId")]
[JsonConverter(typeof(StronglyTypedIdJsonConverter<TripId>))]
public readonly record struct TripId([property: Id(0)] string Value) : IStronglyTypedId<TripId, string>
{
    public const int MaxLength = 64;

    public static TripId From(string value) =>
        TryParse(value, null, out var id) ? id : throw new ArgumentException($"'{value}' is not a valid Trip id.", nameof(value));

    public static TripId Parse(string s, IFormatProvider? provider) =>
        TryParse(s, provider, out var id) ? id : throw new FormatException($"'{s}' is not a valid Trip id.");

    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, out TripId result)
    {
        if (!string.IsNullOrWhiteSpace(s) && s.Length <= MaxLength && s.All(c => char.IsAsciiLetterOrDigit(c) || c is ':' or '_' or '-' or '.'))
        {
            result = new TripId(s);
            return true;
        }

        result = default;
        return false;
    }

    public override string ToString() => Value ?? string.Empty;
}
