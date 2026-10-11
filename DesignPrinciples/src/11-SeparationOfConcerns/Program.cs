using DesignPrinciples.SeparationOfConcerns.Good;

var repository = new InMemoryEmployeeRepository();
var service = new EmployeeOnboardingService(
    new EmployeeValidator(), new PayrollService(), repository, new ConsoleEmployeeNotification());
var employee = service.Onboard("Ada Lovelace", "ada@example.com", 120_000m);
Console.WriteLine($"{employee.Name} onboarded; monthly pay: {employee.MonthlySalary:C}.");
