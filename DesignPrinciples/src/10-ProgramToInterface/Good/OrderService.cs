namespace DesignPrinciples.ProgramToInterface.Good;

public sealed record Product(string Sku, string Name, decimal Price);

public interface IProductCatalog
{
    Product? FindBySku(string sku);
}

public sealed class InMemoryProductCatalog(IEnumerable<Product> products) : IProductCatalog
{
    private readonly Dictionary<string, Product> productsBySku =
        products.ToDictionary(product => product.Sku, StringComparer.OrdinalIgnoreCase);

    public Product? FindBySku(string sku) =>
        productsBySku.GetValueOrDefault(sku);
}

public sealed class OrderService(IProductCatalog catalog)
{
    public decimal GetOrderTotal(string sku, int quantity)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(quantity);
        var product = catalog.FindBySku(sku) ?? throw new InvalidOperationException("Product not found.");
        return product.Price * quantity;
    }
}
