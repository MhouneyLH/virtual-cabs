namespace GleisGrain.Common.Abstractions;

/// <summary>
/// Marks a value type that wraps a primitive identifier. Each context declares its own ids in its Contracts project.
/// </summary>
public interface IStronglyTypedId<TSelf, TValue> : IParsable<TSelf>
    where TSelf : struct, IStronglyTypedId<TSelf, TValue>
    where TValue : notnull
{
    TValue Value { get; }
}
