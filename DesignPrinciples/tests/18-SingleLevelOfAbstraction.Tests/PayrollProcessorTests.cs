using DesignPrinciples.SingleLevelOfAbstraction.Good;

namespace DesignPrinciples.SingleLevelOfAbstraction.Tests;

public sealed class PayrollProcessorTests
{
    [Fact]
    public void ProcessPayroll_CalculatesPersistsAndNotifies()
    {
        var repository = new FakeRepository();
        var sender = new FakeSender();
        var processor = new PayrollProcessor(new FlatRateTaxCalculator(), repository, sender);

        var payroll = processor.ProcessPayroll("ada@example.test", 100m);

        Assert.Equal(20m, payroll.Tax);
        Assert.Equal(80m, payroll.NetPay);
        Assert.Same(payroll, repository.Saved);
        Assert.Same(payroll, sender.Sent);
    }

    private sealed class FakeRepository : IPayrollRepository { public Payroll? Saved { get; private set; } public void Save(Payroll payroll) => Saved = payroll; }
    private sealed class FakeSender : IPayslipSender { public Payroll? Sent { get; private set; } public void Send(Payroll payroll) => Sent = payroll; }
}
