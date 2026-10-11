namespace DesignPrinciples.KISS.Good;

public sealed class DiscountCalculator
{
    public decimal CalculatePercentageDiscount(decimal subtotal, decimal percentage)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(subtotal);
        if (percentage is < 0m or > 100m)
        {
            throw new ArgumentOutOfRangeException(nameof(percentage));
        }

        return decimal.Round(subtotal * percentage / 100m, 2);
    }
}
