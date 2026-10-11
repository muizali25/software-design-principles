namespace DesignPrinciples.SeparationOfConcerns.Good;

public interface IEmployeeNotification { void SendWelcome(Employee employee); }
public sealed class ConsoleEmployeeNotification : IEmployeeNotification
{
    public void SendWelcome(Employee employee) => Console.WriteLine($"Welcome email sent to {employee.Email}.");
}
