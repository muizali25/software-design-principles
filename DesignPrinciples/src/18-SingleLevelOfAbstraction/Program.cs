using DesignPrinciples.SingleLevelOfAbstraction.Good;

var processor = new PayrollProcessor(new FlatRateTaxCalculator(), new ConsoleRepository(), new ConsolePayslipSender());
Console.WriteLine(processor.ProcessPayroll("ada@example.test", 1_000m));

file sealed class ConsoleRepository : IPayrollRepository { public void Save(Payroll payroll) { } }
file sealed class ConsolePayslipSender : IPayslipSender { public void Send(Payroll payroll) { } }
