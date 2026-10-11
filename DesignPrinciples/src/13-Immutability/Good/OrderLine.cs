namespace DesignPrinciples.Immutability.Good;

public sealed record OrderLine
{
    public OrderLine(string productCode, Money unitPrice, int quantity)
    {
        if (string.IsNullOrWhiteSpace(productCode)) throw new ArgumentException("Product code is required.", nameof(productCode));
        if (quantity <= 0) throw new ArgumentOutOfRangeException(nameof(quantity));
        ProductCode = productCode;
        UnitPrice = unitPrice;
        Quantity = quantity;
    }

    public string ProductCode { get; init; }
    public Money UnitPrice { get; init; }
    public int Quantity { get; init; }

    public Money Total => UnitPrice.Multiply(Quantity);

    public OrderLine WithQuantity(int quantity)
    {
        if (quantity <= 0) throw new ArgumentOutOfRangeException(nameof(quantity));
        return this with { Quantity = quantity };
    }
}
