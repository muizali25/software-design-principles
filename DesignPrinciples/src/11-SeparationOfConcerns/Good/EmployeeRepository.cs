namespace DesignPrinciples.SeparationOfConcerns.Good;

public interface IEmployeeRepository { void Add(Employee employee); }
public sealed class InMemoryEmployeeRepository : IEmployeeRepository
{
    private readonly List<Employee> _employees = [];
    public IReadOnlyList<Employee> Employees => _employees;
    public void Add(Employee employee) => _employees.Add(employee);
}
