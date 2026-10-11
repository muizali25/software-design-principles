namespace DesignPrinciples.TellDontAsk.Bad;

public sealed class Order { public string Status = "New"; public bool Paid; public List<string> Items = []; }
public sealed class OrderShippingService
{
    // PROBLEM: Rules and state changes are outside the object that owns the state.
    public void Ship(Order order)
    {
        if (order.Status != "New" || !order.Paid || order.Items.Count == 0) throw new InvalidOperationException("Cannot ship.");
        order.Status = "Shipped";
    }
}
