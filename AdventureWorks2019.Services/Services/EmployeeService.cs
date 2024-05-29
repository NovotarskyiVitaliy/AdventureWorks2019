using AdventureWorks2019.DataAccess.Entities;
using AdventureWorks2019.Domain.Abstractions;
using AdventureWorks2019.Domain.Models;
using AutoMapper;
using Microsoft.Extensions.Options;

namespace AdventureWorks2019.Application.Services;

public class EmployeeService(IEmployeeRepo employeeRepo, IMapper mapper, IJwtProvider jwtProvider) : IEmployeeService
{
    public async Task<IEnumerable<Employee>?> GetEmployees(int? departmentIdc)
    {

        var employees = mapper.Map<IEnumerable<EmployeeEntity>, IEnumerable<Employee>>(await employeeRepo.LoadData<EmployeeEntity>(departmentIdc));

        return employees;
    }

    public async Task UpdateEmployee(Employee employee)
    {
        await employeeRepo.Update(employee);
    }

    public string Login(string user, string hashPassword)
    {
        return jwtProvider.GenerateToken(new UserInfo{User=user, HashPassword = hashPassword});
    }
}