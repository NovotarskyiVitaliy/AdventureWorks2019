using System.Data;
using System.Data.SqlClient;
using AdventureWorks2019.DataAccess.Entities;
using AdventureWorks2019.Domain.Abstractions;
using AdventureWorks2019.Domain.Models;
using AutoMapper;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace AdventureWorks2019.DataAccess.Repositories;

public class EmployeeRepo : IEmployeeRepo
{
    private readonly string _connectionString;
    private readonly IMapper _mapper;

    public EmployeeRepo(IConfiguration configuration, IMapper mapper)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection")
                            ?? throw new InvalidOperationException("ConnectionStrings:DefaultConnection is not configured");
        _mapper = mapper;
    }

    public async Task<IEnumerable<T>> LoadData<T>(int? departmentId)
    {
        using IDbConnection connection = new SqlConnection(_connectionString);

        const string sql = """
            select e.BusinessEntityID, p.FirstName, p.LastName, d.Name, d.GroupName, d.DepartmentID
            from HumanResources.Employee e
            inner join Person.Person p on e.BusinessEntityID = p.BusinessEntityID
            inner join HumanResources.EmployeeDepartmentHistory dh on e.BusinessEntityID = dh.BusinessEntityID
            inner join HumanResources.Department d on d.DepartmentID = dh.DepartmentID
            where (@DepartmentId is null or d.DepartmentID = @DepartmentId)
            """;

        return await connection.QueryAsync<T>(sql, new { DepartmentId = departmentId });
    }

    public async Task<int> Update(Employee employee)
    {
        using IDbConnection connection = new SqlConnection(_connectionString);
        string updateSql = "update Person.Person set FirstName=@firstName, LastName=@lastName where BusinessEntityId=@businessEntityId";

        var employeeEntity = _mapper.Map<EmployeeEntity>(employee);

        return await connection.ExecuteAsync(updateSql,
            new
            {
                firstName = employeeEntity.FirstName, lastName = employeeEntity.LastName, businessEntityId = employeeEntity.BusinessEntityId
            });
    }
}