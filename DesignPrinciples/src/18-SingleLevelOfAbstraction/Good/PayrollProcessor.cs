namespace DesignPrinciples.SingleLevelOfAbstraction.Good;

public sealed record Payroll(string EmployeeEmail, decimal GrossPay, decimal Tax, decimal NetPay);

public interface ITaxCalculator { decimal Calculate(decimal grossPay); }
public interface IPayrollRepository { void Save(Payroll payroll); }
public interface IPayslipSender { void Send(Payroll payroll); }

public sealed class PayrollProcessor(ITaxCalculator taxCalculator, IPayrollRepository repository, IPayslipSender payslipSender)
{
    public Payroll ProcessPayroll(string employeeEmail, decimal grossPay)
    {
        Validate(employeeEmail, grossPay);
        var payroll = CalculatePayroll(employeeEmail, grossPay);
        Persist(payroll);
        NotifyEmployee(payroll);
        return payroll;
    }

    private static void Validate(string employeeEmail, decimal grossPay)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(employeeEmail);
        if (grossPay < 0) throw new ArgumentOutOfRangeException(nameof(grossPay));
    }

    private Payroll CalculatePayroll(string employeeEmail, decimal grossPay)
    {
        var tax = taxCalculator.Calculate(grossPay);
        return new Payroll(employeeEmail, grossPay, tax, grossPay - tax);
    }

    private void Persist(Payroll payroll) => repository.Save(payroll);
    private void NotifyEmployee(Payroll payroll) => payslipSender.Send(payroll);
}

public sealed class FlatRateTaxCalculator : ITaxCalculator
{
    public decimal Calculate(decimal grossPay) => grossPay * 0.20m;
}
