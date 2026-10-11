using DesignPrinciples.KISS.Good;

namespace DesignPrinciples.KISS.Tests;

public sealed class DiscountCalculatorTests
{
    [Theory]
    [InlineData(200, 10, 20)]
    [InlineData(19.99, 15, 3.00)]
    public void CalculatePercentageDiscount_ReturnsRoundedAmount(
        decimal subtotal, decimal percentage, decimal expected)
    {
        Assert.Equal(expected,
            new DiscountCalculator().CalculatePercentageDiscount(subtotal, percentage));
    }

    [Fact]
    public void CalculatePercentageDiscount_RejectsInvalidPercentage()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            new DiscountCalculator().CalculatePercentageDiscount(100m, 101m));
    }
}
