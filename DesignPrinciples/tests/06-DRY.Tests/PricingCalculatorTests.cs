using DesignPrinciples.DRY.Good;

namespace DesignPrinciples.DRY.Tests;

public sealed class PricingCalculatorTests
{
    [Fact]
    public void CalculateTotal_AppliesDiscountThenTax()
    {
        var total = new PricingCalculator().CalculateTotal(
            [new PriceLine("SKU-1", 100m, 2)], 10m);

        Assert.Equal(194.40m, total);
    }

    [Fact]
    public void Services_UseTheSamePricingRule()
    {
        var calculator = new PricingCalculator();
        var lines = new[] { new PriceLine("SKU-1", 50m, 1) };

        Assert.Equal(
            new CheckoutService(calculator).CalculateTotal(lines, 0m),
            new InvoiceService(calculator).CalculateTotal(lines, 0m));
    }
}
