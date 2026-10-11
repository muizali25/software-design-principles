namespace DesignPrinciples.SRP.Bad;

public sealed record OrderLine(string Sku, decimal UnitPrice, int Quantity);
public sealed record Order(string Id, string CustomerEmail, IReadOnlyList<OrderLine> Lines);

public sealed class OrderService
{
    public decimal Process(Order order)
    {
        // PROBLEM: pricing, persistence, email, and invoicing change for unrelated reasons.
        var total = order.Lines.Sum(line => line.UnitPrice * line.Quantity);
        Console.WriteLine($"INSERT order {order.Id} with total {total:C}");
        Console.WriteLine($"Email receipt to {order.CustomerEmail}");
        Console.WriteLine($"Generate invoice for {order.Id}");
        return total;
    }
}
