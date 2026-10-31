namespace GleisGrain.Ingestion.Contracts;

/// <remarks>The grain key is the <see cref="SourceId"/> value.</remarks>
[Alias("GleisGrain.Ingestion.Contracts.ISourceGrain")]
public interface ISourceGrain : IGrainWithStringKey
{
    [Alias("GetAsync")]
    Task<SourceSnapshot?> GetAsync();
}
