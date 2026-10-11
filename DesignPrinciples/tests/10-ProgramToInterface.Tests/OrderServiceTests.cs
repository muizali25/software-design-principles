using DesignPrinciples.ProgramToInterface.Good;

namespace DesignPrinciples.ProgramToInterface.Tests;

public sealed class OrderServiceTests
{
    [Fact]
    public void GetOrderTotal_UsesInjectedCatalog()
    {
        IProductCatalog catalog = new InMemoryProductCatalog(
            [new Product("SKU-1", "Monitor", 300m)]);
        var service = new OrderService(catalog);

        Assert.Equal(600m, service.GetOrderTotal("SKU-1", 2));
    }

    [Fact]
    public void GetOrderTotal_ThrowsForUnknownProduct()
    {
        var service = new OrderService(new InMemoryProductCatalog([]));

        Assert.Throws<InvalidOperationException>(() => service.GetOrderTotal("UNKNOWN", 1));
    }
}
