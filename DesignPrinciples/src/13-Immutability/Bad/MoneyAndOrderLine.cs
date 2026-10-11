namespace DesignPrinciples.Immutability.Bad;

public sealed class Money { public decimal Amount; public string Currency = "USD"; }
public sealed class OrderLine
{
    public Money UnitPrice = new();
    public int Quantity;
    public decimal Total => UnitPrice.Amount * Quantity; // PROBLEM: shared UnitPrice mutations silently change totals.
}
