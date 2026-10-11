using DesignPrinciples.OCP.Good;

namespace DesignPrinciples.OCP.Tests;

public sealed class PaymentProcessorTests
{
    [Fact]
    public void Process_Delegates_To_The_Registered_Processor()
    {
        var service = new PaymentService([new PayPalProcessor()]);

        Assert.Contains("PayPal", service.Process("paypal", new Payment(25m)));
    }

    [Fact]
    public void Process_Rejects_An_Unregistered_Method()
    {
        var service = new PaymentService([]);

        Assert.Throws<NotSupportedException>(() => service.Process("crypto", new Payment(25m)));
    }
}
