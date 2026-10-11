using DesignPrinciples.SeparationOfConcerns.Good;

namespace DesignPrinciples.SeparationOfConcerns.Tests;

public sealed class EmployeeOnboardingServiceTests
{
    [Fact]
    public void Onboard_ValidEmployee_CalculatesPayAndPersists()
    {
        var repository = new InMemoryEmployeeRepository();
        var service = new EmployeeOnboardingService(new EmployeeValidator(), new PayrollService(), repository, new SilentNotification());

        var employee = service.Onboard("Ada", "ada@example.com", 120_000m);

        Assert.Equal(10_000m, employee.MonthlySalary);
        Assert.Single(repository.Employees);
    }

    [Fact]
    public void Onboard_InvalidEmail_ThrowsBeforePersistence()
    {
        var repository = new InMemoryEmployeeRepository();
        var service = new EmployeeOnboardingService(new EmployeeValidator(), new PayrollService(), repository, new SilentNotification());

        Assert.Throws<ArgumentException>(() => service.Onboard("Ada", "invalid", 120_000m));
        Assert.Empty(repository.Employees);
    }

    private sealed class SilentNotification : IEmployeeNotification { public void SendWelcome(Employee employee) { } }
}
