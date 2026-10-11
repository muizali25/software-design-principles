namespace DesignPrinciples.SeparationOfConcerns.Good;

public sealed class PayrollService
{
    public decimal CalculateMonthlySalary(decimal annualSalary) => Math.Round(annualSalary / 12, 2);
}
