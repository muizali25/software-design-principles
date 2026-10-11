namespace DesignPrinciples.ExplicitDependencies.Good;

public sealed record Invoice(string CustomerEmail, decimal Amount);

public interface IInvoiceRepository { void Save(Invoice invoice); }
public interface IEmailSender { void Send(string recipient, string subject, string body); }

public sealed class InvoiceService
{
    private readonly IInvoiceRepository _repository;
    private readonly IEmailSender _emailSender;

    public InvoiceService(IInvoiceRepository repository, IEmailSender emailSender)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _emailSender = emailSender ?? throw new ArgumentNullException(nameof(emailSender));
    }

    public void Send(Invoice invoice)
    {
        ArgumentNullException.ThrowIfNull(invoice);
        _repository.Save(invoice);
        _emailSender.Send(invoice.CustomerEmail, "Your invoice", $"Invoice total: {invoice.Amount:C}.");
    }
}
