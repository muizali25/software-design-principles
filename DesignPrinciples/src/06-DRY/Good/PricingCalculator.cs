namespace DesignPrinciples.DRY.Good;

public sealed record PriceLine(string Sku, decimal UnitPrice, int Quantity);

public sealed class PricingCalculator
{
    private const decimal TaxRate = 0.08m;

    public decimal CalculateTotal(IEnumerable<PriceLine> lines, decimal discountPercent)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(discountPercent);
        if (discountPercent > 100m)
        {
            throw new ArgumentOutOfRangeException(nameof(discountPercent));
        }

        var subtotal = lines.Sum(line => line.UnitPrice * line.Quantity);
        var discounted = subtotal * (1m - discountPercent / 100m);
        return decimal.Round(discounted * (1m + TaxRate), 2);
    }
}

public sealed class CheckoutService(PricingCalculator pricingCalculator)
{
    public decimal CalculateTotal(IEnumerable<PriceLine> lines, decimal discountPercent) =>
        pricingCalculator.CalculateTotal(lines, discountPercent);
}

public sealed class InvoiceService(PricingCalculator pricingCalculator)
{
    public decimal CalculateTotal(IEnumerable<PriceLine> lines, decimal discountPercent) =>
        pricingCalculator.CalculateTotal(lines, discountPercent);
}
