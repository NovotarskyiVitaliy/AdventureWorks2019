using AdventureWorks2019.Domain.Models;

namespace AdventureWorks2019.Application.Services;

public interface IEmployeeService
{
    Task<IEnumerable<Employee>?> GetEmployees(int? departmentId);
    Task<bool> UpdateEmployee(Employee employee);
    string? Login(string user, string password);
}
