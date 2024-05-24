using AdventureWorks2019.Domain.Models;
using AutoMapper;

namespace AdventureWorks2019.Application.Data;

public interface IDataEmployeeService
{
    Task<IEnumerable<Employee>?> GetEmployee(int? departmentIdc);
    Task Update(Employee employee);
}