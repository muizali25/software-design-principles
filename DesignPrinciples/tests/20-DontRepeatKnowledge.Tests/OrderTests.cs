using DesignPrinciples.DontRepeatKnowledge.Good;

namespace DesignPrinciples.DontRepeatKnowledge.Tests;

public sealed class OrderTests
{
    [Fact]
    public void PaidOrder_CanBeCancelledThroughSingleDomainRule()
    {
        var order = new Order(OrderStatus.Paid);

        order.Cancel();

        Assert.Equal(OrderStatus.Cancelled, order.Status);
    }

    [Fact]
    public void ShippedOrder_CannotBeCancelled()
    {
        var order = new Order(OrderStatus.Shipped);

        Assert.False(order.IsCancellable);
        Assert.Throws<InvalidOperationException>(order.Cancel);
    }
}
