namespace DesignPrinciples.LawOfDemeter.Good;

public sealed class ShippingLabelPrinter
{
    public string Print(Order order) => $"Order {order.OrderNumber}: ship to {order.GetShippingDestination().Format()}";
}
