namespace DesignPrinciples.SingleLevelOfAbstraction.Bad;

public sealed class PayrollProcessor
{
    public string ProcessPayroll(string employeeEmail, decimal grossPay)
    {
        var tax = grossPay * 0.20m;
        var net = grossPay - tax;
        var sql = $"INSERT INTO Payroll (Email, Gross, Tax, Net) VALUES ('{employeeEmail}', {grossPay}, {tax}, {net})";
        var smtpHost = "smtp.company.test";
        return $"Executed {sql}; sent payslip through {smtpHost} to {employeeEmail}.";
    }
}
