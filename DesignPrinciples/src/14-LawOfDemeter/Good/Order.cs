namespace DesignPrinciples.LawOfDemeter.Good;

public sealed class Order(string orderNumber, ShippingAddress shippingAddress)
{
    public string OrderNumber { get; } = orderNumber;
    private readonly ShippingAddress _shippingAddress = shippingAddress;
    public ShippingAddress GetShippingDestination() => _shippingAddress;
}
