namespace GleisGrain.Common.Errors;

public sealed class DomainException(DomainError error) : Exception(error.Detail)
{
    public DomainError Error { get; } = error;
}
