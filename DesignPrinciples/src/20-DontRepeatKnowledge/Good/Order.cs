namespace DesignPrinciples.DontRepeatKnowledge.Good;

public enum OrderStatus { Draft, Paid, Shipped, Cancelled }

public sealed class Order
{
    public Order(OrderStatus status) => Status = status;

    public OrderStatus Status { get; private set; }
    public bool IsCancellable => Status is OrderStatus.Draft or OrderStatus.Paid;

    public void Cancel()
    {
        if (!IsCancellable)
        {
            throw new InvalidOperationException($"An order in {Status} status cannot be cancelled.");
        }

        Status = OrderStatus.Cancelled;
    }
}
