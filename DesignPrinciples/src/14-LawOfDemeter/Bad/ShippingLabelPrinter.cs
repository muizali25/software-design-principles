namespace DesignPrinciples.LawOfDemeter.Bad;

public sealed class Address { public string City { get; init; } = ""; public string Zip { get; init; } = ""; }
public sealed class Customer { public Address Address { get; init; } = new(); }
public sealed class Order { public Customer Customer { get; init; } = new(); }
public sealed class ShippingLabelPrinter
{
    // PROBLEM: The printer knows the internal navigation of Order, Customer, and Address.
    public string Print(Order order) => $"Ship to: {order.Customer.Address.City}, {order.Customer.Address.Zip}";
}
