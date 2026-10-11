namespace DesignPrinciples.SRP.Good;

public sealed record OrderLine(string Sku, decimal UnitPrice, int Quantity);
public sealed record Order(string Id, string CustomerEmail, IReadOnlyList<OrderLine> Lines);

public sealed class OrderTotalCalculator
{
    public decimal Calculate(Order order) => order.Lines.Sum(line => line.UnitPrice * line.Quantity);
}

public interface IOrderRepository { void Save(Order order, decimal total); }
public interface IReceiptSender { void Send(Order order, decimal total); }
public interface IInvoiceGenerator { string Generate(Order order, decimal total); }

public sealed class OrderProcessor(
    OrderTotalCalculator calculator,
    IOrderRepository repository,
    IReceiptSender receiptSender,
    IInvoiceGenerator invoiceGenerator)
{
    public string Process(Order order)
    {
        var total = calculator.Calculate(order);
        repository.Save(order, total);
        receiptSender.Send(order, total);
        return invoiceGenerator.Generate(order, total);
    }
}

public sealed class ConsoleOrderRepository : IOrderRepository
{
    public void Save(Order order, decimal total) => Console.WriteLine($"Saved {order.Id}: {total:C}");
}

public sealed class ConsoleReceiptSender : IReceiptSender
{
    public void Send(Order order, decimal total) => Console.WriteLine($"Receipt sent to {order.CustomerEmail}");
}

public sealed class SimpleInvoiceGenerator : IInvoiceGenerator
{
    public string Generate(Order order, decimal total) => $"Invoice {order.Id}: {total:C}";
}
