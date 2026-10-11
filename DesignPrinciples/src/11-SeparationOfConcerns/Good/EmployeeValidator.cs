namespace DesignPrinciples.SeparationOfConcerns.Good;

public sealed class EmployeeValidator
{
    public void Validate(string name, string email, decimal annualSalary)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name is required.", nameof(name));
        if (string.IsNullOrWhiteSpace(email) || !email.Contains('@')) throw new ArgumentException("A valid email is required.", nameof(email));
        if (annualSalary <= 0) throw new ArgumentOutOfRangeException(nameof(annualSalary));
    }
}
