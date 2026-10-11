using DesignPrinciples.TellDontAsk.Good;

namespace DesignPrinciples.TellDontAsk.Tests;

public sealed class OrderTests
{
    [Fact]
    public void Ship_PaidOrderWithItems_ChangesStatus()
    {
        var order = new Order();
        order.AddItem("SKU-1");
        order.MarkPaid();
        order.Ship();
        Assert.Equal(OrderStatus.Shipped, order.Status);
    }

    [Fact]
    public void Ship_UnpaidOrder_RejectsInvalidTransition()
    {
        var order = new Order();
        order.AddItem("SKU-1");
        Assert.Throws<InvalidOperationException>(order.Ship);
        Assert.Equal(OrderStatus.New, order.Status);
    }
}
