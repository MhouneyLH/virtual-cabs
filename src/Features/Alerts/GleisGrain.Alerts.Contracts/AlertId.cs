using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using GleisGrain.Common.Abstractions;

namespace GleisGrain.Alerts.Contracts;

[GenerateSerializer, Immutable, Alias("GleisGrain.Alerts.Contracts.AlertId")]
[JsonConverter(typeof(StronglyTypedIdJsonConverter<AlertId>))]
public readonly record struct AlertId([property: Id(0)] string Value) : IStronglyTypedId<AlertId, string>
{
    public const int MaxLength = 64;

    public static AlertId From(string value) =>
        TryParse(value, null, out var id) ? id : throw new ArgumentException($"'{value}' is not a valid Alert id.", nameof(value));

    public static AlertId Parse(string s, IFormatProvider? provider) =>
        TryParse(s, provider, out var id) ? id : throw new FormatException($"'{s}' is not a valid Alert id.");

    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, out AlertId result)
    {
        if (!string.IsNullOrWhiteSpace(s) && s.Length <= MaxLength && s.All(c => char.IsAsciiLetterOrDigit(c) || c is ':' or '_' or '-' or '.'))
        {
            result = new AlertId(s);
            return true;
        }

        result = default;
        return false;
    }

    public override string ToString() => Value ?? string.Empty;
}
