using EmployeeApi.Models;

namespace EmployeeApi.Services;

public interface IEmployeeService
{
    IReadOnlyList<Employee> GetEmployees();
    Employee? GetEmployeeById(int id);
    Task<Employee?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
}
