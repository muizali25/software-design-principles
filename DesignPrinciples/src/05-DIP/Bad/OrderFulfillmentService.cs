namespace DesignPrinciples.DIP.Bad;

public sealed record FulfillmentOrder(string Id, string CustomerEmail);

public sealed class SqlOrderRepository
{
    public void MarkFulfilled(string orderId) => Console.WriteLine($"Updated SQL order {orderId}");
}

public sealed class SmtpEmailSender
{
    public void Send(string address, string message) => Console.WriteLine($"SMTP to {address}: {message}");
}

public sealed class OrderFulfillmentService
{
    public void Fulfill(FulfillmentOrder order)
    {
        // PROBLEM: policy directly depends on concrete infrastructure and cannot be isolated in tests.
        var repository = new SqlOrderRepository();
        var emailSender = new SmtpEmailSender();
        repository.MarkFulfilled(order.Id);
        emailSender.Send(order.CustomerEmail, "Your order has shipped.");
    }
}
