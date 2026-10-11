namespace DesignPrinciples.ProgramToInterface.Bad;

public sealed record Product(string Sku, string Name, decimal Price);

public sealed class SqlProductCatalog
{
    public Product? FindBySku(string sku) =>
        sku == "LAPTOP-15" ? new Product(sku, "Laptop", 1_200m) : null;
}

public sealed class OrderService
{
    private readonly SqlProductCatalog catalog = new();

    // PROBLEM: The order workflow is coupled to SQL construction and cannot use another catalog.
    public decimal GetOrderTotal(string sku, int quantity)
    {
        var product = catalog.FindBySku(sku) ?? throw new InvalidOperationException("Product not found.");
        return product.Price * quantity;
    }
}
