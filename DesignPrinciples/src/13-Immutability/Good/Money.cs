namespace DesignPrinciples.Immutability.Good;

public readonly record struct Money
{
    public Money(decimal amount, string currency)
    {
        if (amount < 0) throw new ArgumentOutOfRangeException(nameof(amount));
        if (string.IsNullOrWhiteSpace(currency)) throw new ArgumentException("Currency is required.", nameof(currency));
        Amount = amount;
        Currency = currency;
    }

    public decimal Amount { get; }
    public string Currency { get; }

    public Money Multiply(int multiplier) => multiplier < 0
        ? throw new ArgumentOutOfRangeException(nameof(multiplier))
        : new Money(Amount * multiplier, Currency);
}
