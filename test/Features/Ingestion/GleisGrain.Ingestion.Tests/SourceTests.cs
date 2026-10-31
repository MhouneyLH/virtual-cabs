using GleisGrain.Ingestion.Contracts;
using GleisGrain.Ingestion.Domain;

namespace GleisGrain.Ingestion.Tests;

public class SourceTests
{
    [Fact]
    public void ToSnapshot_ReturnsIdAndCreationTime()
    {
        var createdAt = new DateTimeOffset(2026, 1, 1, 0, 0, 0, TimeSpan.Zero);
        var id = SourceId.From("8f3a1c");
        var source = new Source(id, new SourceState { CreatedAt = createdAt });

        var snapshot = source.ToSnapshot();

        Assert.Equal(new SourceSnapshot(id, createdAt), snapshot);
    }
}
