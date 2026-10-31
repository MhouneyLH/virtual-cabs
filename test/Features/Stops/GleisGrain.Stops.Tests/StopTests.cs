using GleisGrain.Stops.Contracts;
using GleisGrain.Stops.Domain;

namespace GleisGrain.Stops.Tests;

public class StopTests
{
    [Fact]
    public void ToSnapshot_ReturnsIdAndCreationTime()
    {
        var createdAt = new DateTimeOffset(2026, 1, 1, 0, 0, 0, TimeSpan.Zero);
        var id = StopId.From("8f3a1c");
        var stop = new Stop(id, new StopState { CreatedAt = createdAt });

        var snapshot = stop.ToSnapshot();

        Assert.Equal(new StopSnapshot(id, createdAt), snapshot);
    }
}
