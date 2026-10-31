using System.ComponentModel.DataAnnotations;

namespace GleisGrain.Common.Options;

public sealed class HttpOptions
{
    public const string SectionName = "Http";

    [Range(1, 300)]
    public int RequestTimeoutSeconds { get; init; } = 30;

    public bool EnableApiReference { get; init; }
}
