namespace DesignPrinciples.DRY.Bad;

public sealed record OrderLine(string Sku, decimal UnitPrice, int Quantity);

public sealed class CheckoutService
{
    // PROBLEM: This pricing rule is copied rather than owned by one component.
    public decimal CalculateTotal(IEnumerable<OrderLine> lines, decimal discountPercent)
    {
        var subtotal = lines.Sum(line => line.UnitPrice * line.Quantity);
        var discounted = subtotal - (subtotal * discountPercent / 100m);
        var tax = discounted * 0.08m;
        return decimal.Round(discounted + tax, 2);
    }
}

public sealed class InvoiceService
{
    // PROBLEM: A subtle tax-rate difference now produces inconsistent customer totals.
    public decimal CalculateTotal(IEnumerable<OrderLine> lines, decimal discountPercent)
    {
        var subtotal = lines.Sum(line => line.UnitPrice * line.Quantity);
        var discounted = subtotal - (subtotal * discountPercent / 100m);
        var tax = discounted * 0.075m;
        return decimal.Round(discounted + tax, 2);
    }
}
