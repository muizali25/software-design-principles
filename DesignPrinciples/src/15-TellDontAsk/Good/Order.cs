namespace DesignPrinciples.TellDontAsk.Good;

public enum OrderStatus { New, Paid, Shipped }

public sealed class Order
{
    private readonly List<string> _items = [];
    public OrderStatus Status { get; private set; } = OrderStatus.New;
    public IReadOnlyList<string> Items => _items;

    public void AddItem(string sku)
    {
        if (string.IsNullOrWhiteSpace(sku)) throw new ArgumentException("SKU is required.", nameof(sku));
        if (Status == OrderStatus.Shipped) throw new InvalidOperationException("A shipped order cannot change.");
        _items.Add(sku);
    }

    public void MarkPaid()
    {
        if (Status != OrderStatus.New) throw new InvalidOperationException("Only new orders can be paid.");
        Status = OrderStatus.Paid;
    }

    public void Ship()
    {
        if (Status != OrderStatus.Paid) throw new InvalidOperationException("Only paid orders can ship.");
        if (_items.Count == 0) throw new InvalidOperationException("An order needs an item before shipping.");
        Status = OrderStatus.Shipped;
    }
}
