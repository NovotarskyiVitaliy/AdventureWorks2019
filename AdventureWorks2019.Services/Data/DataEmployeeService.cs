using AdventureWorks2019.DataAccess.Entities;
using AdventureWorks2019.Domain.Abstractions;
using AdventureWorks2019.Domain.Models;
using AutoMapper;

namespace AdventureWorks2019.Application.Data;

public class DataEmployeeService(IEmployeeRepo employeeRepo, IMapper mapper) : IDataEmployeeService
{
    public async Task<IEnumerable<Employee>?> GetEmployee(int? departmentIdc)
    {

        var employees = mapper.Map<IEnumerable<EmployeeEntity>, IEnumerable<Employee>>(await employeeRepo.LoadData<EmployeeEntity>(departmentIdc));

        return employees;
    }

    public async Task Update(Employee employee)
    {
        await employeeRepo.Update(employee);
    }
}