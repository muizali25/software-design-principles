namespace DesignPrinciples.TellDontAsk.Good;

public sealed class OrderShippingService
{
    public void Ship(Order order) => order.Ship();
}
