namespace DesignPrinciples.FailFast.Bad;

public sealed record AcceptedOrder(string CustomerName, int Quantity);

public sealed class OrderIntakeService
{
    public AcceptedOrder Accept(string? customerName, int quantity)
    {
        var safeCustomer = string.IsNullOrWhiteSpace(customerName) ? "Walk-in customer" : customerName;
        var safeQuantity = Math.Max(quantity, 0);
        return new AcceptedOrder(safeCustomer, safeQuantity);
    }
}
