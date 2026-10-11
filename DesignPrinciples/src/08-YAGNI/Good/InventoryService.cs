namespace DesignPrinciples.YAGNI.Good;

public sealed class InventoryService
{
    private readonly Dictionary<string, int> stock = new(StringComparer.OrdinalIgnoreCase);

    public int GetAvailable(string sku) => stock.GetValueOrDefault(sku);

    public int Adjust(string sku, int change)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(sku);
        var updatedQuantity = GetAvailable(sku) + change;
        if (updatedQuantity < 0)
        {
            throw new InvalidOperationException("Stock cannot be negative.");
        }

        stock[sku] = updatedQuantity;
        return updatedQuantity;
    }
}
