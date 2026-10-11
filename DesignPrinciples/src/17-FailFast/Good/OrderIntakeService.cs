namespace DesignPrinciples.FailFast.Good;

public sealed record AcceptedOrder(string CustomerName, int Quantity);

public sealed class OrderIntakeService
{
    public AcceptedOrder Accept(string? customerName, int quantity)
    {
        if (string.IsNullOrWhiteSpace(customerName))
        {
            throw new ArgumentException("A customer name is required.", nameof(customerName));
        }

        if (quantity <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(quantity), quantity, "Quantity must be greater than zero.");
        }

        return new AcceptedOrder(customerName, quantity);
    }
}
