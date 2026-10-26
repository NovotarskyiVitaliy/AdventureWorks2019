
using AdventureWorks2019.Domain.Models;

namespace AdventureWorks2019.Domain.Abstractions;

public interface IEmployeeRepo
{
    Task<IEnumerable<T>> LoadData<T>(int? departmentId);
    Task<int> Update(Employee employee);
}