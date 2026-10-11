using DesignPrinciples.YAGNI.Good;

namespace DesignPrinciples.YAGNI.Tests;

public sealed class InventoryServiceTests
{
    [Fact]
    public void Adjust_ChangesAvailableStock()
    {
        var inventory = new InventoryService();

        inventory.Adjust("SKU-1", 10);
        var result = inventory.Adjust("SKU-1", -4);

        Assert.Equal(6, result);
        Assert.Equal(6, inventory.GetAvailable("SKU-1"));
    }

    [Fact]
    public void Adjust_RejectsNegativeStock()
    {
        var inventory = new InventoryService();

        Assert.Throws<InvalidOperationException>(() => inventory.Adjust("SKU-1", -1));
    }
}
