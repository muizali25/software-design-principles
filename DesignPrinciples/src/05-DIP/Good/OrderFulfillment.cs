namespace DesignPrinciples.DIP.Good;

public sealed record FulfillmentOrder(string Id, string CustomerEmail);
public interface IOrderRepository { void MarkFulfilled(string orderId); }
public interface IEmailSender { void Send(string address, string message); }

public sealed class OrderFulfillmentService(IOrderRepository repository, IEmailSender emailSender)
{
    public void Fulfill(FulfillmentOrder order)
    {
        repository.MarkFulfilled(order.Id);
        emailSender.Send(order.CustomerEmail, "Your order has shipped.");
    }
}

public sealed class ConsoleOrderRepository : IOrderRepository
{
    public void MarkFulfilled(string orderId) => Console.WriteLine($"Updated order {orderId}");
}

public sealed class ConsoleEmailSender : IEmailSender
{
    public void Send(string address, string message) => Console.WriteLine($"Email to {address}: {message}");
}
