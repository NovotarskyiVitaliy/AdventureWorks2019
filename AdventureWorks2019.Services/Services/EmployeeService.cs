using System.Security.Cryptography;
using System.Text;
using AdventureWorks2019.DataAccess.Entities;
using AdventureWorks2019.Domain.Abstractions;
using AdventureWorks2019.Domain.Models;
using AutoMapper;
using Microsoft.Extensions.Options;

namespace AdventureWorks2019.Application.Services;

public class EmployeeService(
    IEmployeeRepo employeeRepo,
    IMapper mapper,
    IJwtProvider jwtProvider,
    IOptions<AuthOptions> authOptions) : IEmployeeService
{
    private readonly AuthOptions _auth = authOptions.Value;

    public async Task<IEnumerable<Employee>?> GetEmployees(int? departmentId)
    {
        return mapper.Map<IEnumerable<Employee>>(await employeeRepo.LoadData<EmployeeEntity>(departmentId));
    }

    public async Task<bool> UpdateEmployee(Employee employee)
    {
        return await employeeRepo.Update(employee) > 0;
    }

    public string? Login(string user, string password)
    {
        if (string.IsNullOrEmpty(_auth.User) || string.IsNullOrEmpty(_auth.Password))
            return null;

        var userOk = FixedTimeEquals(user, _auth.User);
        var passwordOk = FixedTimeEquals(password, _auth.Password);

        return userOk && passwordOk ? jwtProvider.GenerateToken(user) : null;
    }

    private static bool FixedTimeEquals(string a, string b) =>
        CryptographicOperations.FixedTimeEquals(
            SHA256.HashData(Encoding.UTF8.GetBytes(a)),
            SHA256.HashData(Encoding.UTF8.GetBytes(b)));
}
