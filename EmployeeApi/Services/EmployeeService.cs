using EmployeeApi.Models;

namespace EmployeeApi.Services;

public class EmployeeService : IEmployeeService
{
    private static readonly IReadOnlyList<Employee> Employees = Array.AsReadOnly(
        new[]
        {
            new Employee(1, "Raheel","IT", 75000m),
            new Employee(2, "Samad","Finance", 25000m),
            new Employee(3, "Abdul","HR", 55000m),
            new Employee(4, "Mussawir","Supply Chain", 60000m),
        }
     );

    public IReadOnlyList<Employee> GetEmployees() => Employees;

    public Employee? GetEmployeeById(int id) => id <= 0 ? null : Employees.FirstOrDefault(x => x.Id == id);
    public async Task<Employee?> GetByIdAsync(int id, CancellationToken token = default)
    {
        if (id <= 0)
            return null;

        var employee = Employees.FirstOrDefault(x => x.Id == id);

        if (employee != null)
            return employee;

        return null;
    }
}
