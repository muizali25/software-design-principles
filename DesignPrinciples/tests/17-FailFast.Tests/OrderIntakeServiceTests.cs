using DesignPrinciples.FailFast.Good;

namespace DesignPrinciples.FailFast.Tests;

public sealed class OrderIntakeServiceTests
{
    private readonly OrderIntakeService _service = new();

    [Fact]
    public void Accept_ValidOrder_ReturnsOrder()
    {
        var order = _service.Accept("Ada", 3);

        Assert.Equal(new AcceptedOrder("Ada", 3), order);
    }

    [Fact]
    public void Accept_MissingCustomer_ThrowsImmediately() =>
        Assert.Throws<ArgumentException>(() => _service.Accept(null, 3));

    [Fact]
    public void Accept_NonPositiveQuantity_ThrowsImmediately() =>
        Assert.Throws<ArgumentOutOfRangeException>(() => _service.Accept("Ada", 0));
}
