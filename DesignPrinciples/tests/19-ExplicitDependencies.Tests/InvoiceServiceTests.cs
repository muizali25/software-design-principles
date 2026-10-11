using DesignPrinciples.ExplicitDependencies.Good;

namespace DesignPrinciples.ExplicitDependencies.Tests;

public sealed class InvoiceServiceTests
{
    [Fact]
    public void Send_PersistsInvoiceAndSendsEmail()
    {
        var repository = new FakeRepository();
        var emailSender = new FakeEmailSender();
        var invoice = new Invoice("ada@example.test", 49.95m);

        new InvoiceService(repository, emailSender).Send(invoice);

        Assert.Same(invoice, repository.Saved);
        Assert.Equal("ada@example.test", emailSender.Recipient);
        Assert.Equal("Your invoice", emailSender.Subject);
        Assert.Contains("49.95", emailSender.Body);
    }

    [Fact]
    public void Constructor_Requires_Collaborators()
    {
        Assert.Throws<ArgumentNullException>(() => new InvoiceService(null!, new FakeEmailSender()));
        Assert.Throws<ArgumentNullException>(() => new InvoiceService(new FakeRepository(), null!));
    }

    private sealed class FakeRepository : IInvoiceRepository { public Invoice? Saved { get; private set; } public void Save(Invoice invoice) => Saved = invoice; }
    private sealed class FakeEmailSender : IEmailSender
    {
        public string? Recipient { get; private set; }
        public string? Subject { get; private set; }
        public string Body { get; private set; } = string.Empty;
        public void Send(string recipient, string subject, string body)
        {
            Recipient = recipient;
            Subject = subject;
            Body = body;
        }
    }
}
