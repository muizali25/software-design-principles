namespace DesignPrinciples.SeparationOfConcerns.Bad;

public sealed class EmployeeOnboardingController
{
    // PROBLEM: One class owns validation, payroll policy, storage, and communication.
    public string Onboard(string name, string email, decimal annualSalary)
    {
        if (string.IsNullOrWhiteSpace(name) || !email.Contains('@') || annualSalary <= 0)
            return "Invalid employee.";

        var monthlySalary = Math.Round(annualSalary / 12, 2);
        File.AppendAllText("employees.txt", $"{name}|{email}|{monthlySalary}{Environment.NewLine}");
        Console.WriteLine($"Welcome email sent to {email}.");
        return $"Employee onboarded with monthly salary {monthlySalary:C}.";
    }
}
