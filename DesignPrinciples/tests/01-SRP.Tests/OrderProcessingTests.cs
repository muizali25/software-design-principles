using DesignPrinciples.SRP.Good;

namespace DesignPrinciples.SRP.Tests;

public sealed class OrderProcessingTests
{
    [Fact]
    public void Calculate_Sums_All_Line_Items()
    {
        var order = new Order("1", "a@b.com", [new("A", 12.5m, 2), new("B", 5m, 1)]);

        Assert.Equal(30m, new OrderTotalCalculator().Calculate(order));
    }

    [Fact]
    public void Process_Uses_Each_Collaborator()
    {
        var repository = new RecordingRepository();
        var sender = new RecordingSender();
        var invoice = new RecordingInvoiceGenerator();
        var order = new Order("1", "a@b.com", [new("A", 10m, 1)]);

        var result = new OrderProcessor(new OrderTotalCalculator(), repository, sender, invoice).Process(order);

        Assert.Equal("Invoice-1", result);
        Assert.Equal(10m, repository.Total);
        Assert.Equal(10m, sender.Total);
    }

    private sealed class RecordingRepository : IOrderRepository { public decimal Total { get; private set; } public void Save(Order order, decimal total) => Total = total; }
    private sealed class RecordingSender : IReceiptSender { public decimal Total { get; private set; } public void Send(Order order, decimal total) => Total = total; }
    private sealed class RecordingInvoiceGenerator : IInvoiceGenerator { public string Generate(Order order, decimal total) => $"Invoice-{order.Id}"; }
}
