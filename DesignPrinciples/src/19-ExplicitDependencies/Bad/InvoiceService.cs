namespace DesignPrinciples.ExplicitDependencies.Bad;

public sealed record Invoice(string CustomerEmail, decimal Amount);

public static class ServiceLocator
{
    public static object Get(string key) => new object();
}

public sealed class InvoiceService
{
    public void Send(Invoice invoice)
    {
        var repository = ServiceLocator.Get("invoiceRepository");
        var smtpClient = new System.Net.Mail.SmtpClient("smtp.company.test");
        _ = repository;
        _ = smtpClient;
    }
}
