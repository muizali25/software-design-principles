using DesignPrinciples.Immutability.Good;

namespace DesignPrinciples.Immutability.Tests;

public sealed class OrderLineTests
{
    [Fact]
    public void WithQuantity_CreatesNewLine_WithoutChangingOriginal()
    {
        var original = new OrderLine("BOOK", new Money(10m, "USD"), 2);
        var revised = original.WithQuantity(3);
        Assert.Equal(20m, original.Total.Amount);
        Assert.Equal(30m, revised.Total.Amount);
    }

    [Fact]
    public void Money_RejectsNegativeAmount() =>
        Assert.Throws<ArgumentOutOfRangeException>(() => new Money(-1m, "USD"));
}
