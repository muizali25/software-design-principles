using DesignPrinciples.LawOfDemeter.Good;

namespace DesignPrinciples.LawOfDemeter.Tests;

public sealed class ShippingLabelPrinterTests
{
    [Fact]
    public void Print_UsesOrderDestination()
    {
        var order = new Order("ORD-42", new ShippingAddress("1 Main St", "Seattle", "98101"));
        var label = new ShippingLabelPrinter().Print(order);
        Assert.Equal("Order ORD-42: ship to 1 Main St, Seattle 98101", label);
    }
}
