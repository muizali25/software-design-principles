namespace DesignPrinciples.KISS.Bad;

public sealed record Cart(decimal Subtotal);
public sealed record DiscountCalculated(decimal Amount);
public interface IDiscountStrategy { decimal Apply(Cart cart, decimal percentage); }
public interface IDiscountStrategyFactory { IDiscountStrategy Create(string type); }
public interface IEventBus { void Publish(DiscountCalculated @event); }

public sealed class PercentageDiscountStrategy : IDiscountStrategy
{
    public decimal Apply(Cart cart, decimal percentage) => cart.Subtotal * percentage / 100m;
}

public sealed class DiscountStrategyFactory : IDiscountStrategyFactory
{
    public IDiscountStrategy Create(string type) => type == "percentage"
        ? new PercentageDiscountStrategy()
        : throw new ArgumentException("Unknown discount type.", nameof(type));
}

public sealed class DiscountEngine(IDiscountStrategyFactory factory, IEventBus eventBus)
{
    // PROBLEM: Three abstractions and an event are used for one fixed formula.
    public decimal Calculate(Cart cart, decimal percentage)
    {
        var amount = factory.Create("percentage").Apply(cart, percentage);
        eventBus.Publish(new DiscountCalculated(amount));
        return amount;
    }
}
