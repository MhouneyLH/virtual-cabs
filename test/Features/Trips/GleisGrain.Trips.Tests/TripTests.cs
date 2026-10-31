using GleisGrain.Trips.Contracts;
using GleisGrain.Trips.Domain;

namespace GleisGrain.Trips.Tests;

public class TripTests
{
    [Fact]
    public void ToSnapshot_ReturnsIdAndCreationTime()
    {
        var createdAt = new DateTimeOffset(2026, 1, 1, 0, 0, 0, TimeSpan.Zero);
        var id = TripId.From("8f3a1c");
        var trip = new Trip(id, new TripState { CreatedAt = createdAt });

        var snapshot = trip.ToSnapshot();

        Assert.Equal(new TripSnapshot(id, createdAt), snapshot);
    }
}
