namespace DesignPrinciples.YAGNI.Bad;

public interface IInventoryPlugin { void Configure(); }
public interface ICache { int? Get(string key); void Set(string key, int value); }
public interface IEventBus { void Publish(object message); }
public interface IMultiWarehouseSynchronizer { void Synchronize(string sku, int quantity); }

public sealed class InventoryService(
    IEnumerable<IInventoryPlugin> plugins,
    ICache cache,
    IEventBus eventBus,
    IMultiWarehouseSynchronizer synchronizer)
{
    private readonly Dictionary<string, int> stock = new();

    // PROBLEM: A single-warehouse service pays for features it does not use.
    public int Adjust(string sku, int change)
    {
        foreach (var plugin in plugins) plugin.Configure();
        var quantity = stock.GetValueOrDefault(sku) + change;
        stock[sku] = quantity;
        cache.Set(sku, quantity);
        synchronizer.Synchronize(sku, quantity);
        eventBus.Publish(new { sku, quantity });
        return quantity;
    }
}
