using DesignPrinciples.DIP.Good;

namespace DesignPrinciples.DIP.Tests;

public sealed class OrderFulfillmentTests
{
    [Fact]
    public void Fulfill_Persists_And_Notifies_Using_Injected_Abstractions()
    {
        var repository = new FakeRepository();
        var email = new FakeEmailSender();

        new OrderFulfillmentService(repository, email).Fulfill(new FulfillmentOrder("42", "sam@example.com"));

        Assert.Equal("42", repository.OrderId);
        Assert.Equal("sam@example.com", email.Address);
        Assert.Contains("shipped", email.Message);
    }

    private sealed class FakeRepository : IOrderRepository { public string? OrderId { get; private set; } public void MarkFulfilled(string orderId) => OrderId = orderId; }
    private sealed class FakeEmailSender : IEmailSender { public string? Address { get; private set; } public string Message { get; private set; } = string.Empty; public void Send(string address, string message) => (Address, Message) = (address, message); }
}
