using GleisGrain.Alerts.Contracts;
using GleisGrain.Alerts.Domain;

namespace GleisGrain.Alerts.Tests;

public class AlertTests
{
    [Fact]
    public void ToSnapshot_ReturnsIdAndCreationTime()
    {
        var createdAt = new DateTimeOffset(2026, 1, 1, 0, 0, 0, TimeSpan.Zero);
        var id = AlertId.From("8f3a1c");
        var alert = new Alert(id, new AlertState { CreatedAt = createdAt });

        var snapshot = alert.ToSnapshot();

        Assert.Equal(new AlertSnapshot(id, createdAt), snapshot);
    }
}
