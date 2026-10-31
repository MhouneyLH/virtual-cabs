namespace GleisGrain.Common.Errors;

/// <summary>Base for every expected failure. Each feature declares its own sealed records in its <c>Errors.cs</c>.</summary>
public abstract record DomainError(string Code, string Title, int Status, string Detail);
