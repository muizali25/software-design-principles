using DesignPrinciples.ExplicitDependencies.Good;

var service = new InvoiceService(new ConsoleRepository(), new ConsoleEmailSender());
service.Send(new Invoice("ada@example.test", 49.95m));
Console.WriteLine("Invoice stored and email queued.");

file sealed class ConsoleRepository : IInvoiceRepository { public void Save(Invoice invoice) { } }
file sealed class ConsoleEmailSender : IEmailSender { public void Send(string recipient, string subject, string body) { } }
