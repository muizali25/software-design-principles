namespace DesignPrinciples.SeparationOfConcerns.Good;

public sealed class EmployeeOnboardingService(
    EmployeeValidator validator,
    PayrollService payroll,
    IEmployeeRepository repository,
    IEmployeeNotification notification)
{
    public Employee Onboard(string name, string email, decimal annualSalary)
    {
        validator.Validate(name, email, annualSalary);
        var employee = new Employee(name, email, annualSalary, payroll.CalculateMonthlySalary(annualSalary));
        repository.Add(employee);
        notification.SendWelcome(employee);
        return employee;
    }
}
