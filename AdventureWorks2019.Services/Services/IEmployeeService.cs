using AdventureWorks2019.Domain.Models;
using Microsoft.Extensions.Options;

namespace AdventureWorks2019.Application.Services;

public interface IEmployeeService
{
    Task<IEnumerable<Employee>?> GetEmployees(int? departmentIdc);
    Task UpdateEmployee(Employee employee);
    string Login(string user, string  hashPassword);
}