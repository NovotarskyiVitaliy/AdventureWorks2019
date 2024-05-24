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

    //public EmployeeRepo(string connectionString, IMapper mapper)
    public EmployeeRepo(IConfiguration configurationManager, IMapper mapper)
    {
        //_connectionString = configurationManager.GetConnectionString("DefaultConnection");
        _connectionString = Environment.GetEnvironmentVariable("DefaultConnection");
        _mapper = mapper;
    }
    public async Task<IEnumerable<T>> LoadData<T>(int? departmentId)
    {
        Console.WriteLine(_connectionString);

        using IDbConnection connection = new SqlConnection(_connectionString);

        string sql = "select e.BusinessEntityID, p.FirstName, p.LastName, d.Name, d.GroupName, d.DepartmentID\r\n" +
                     "from  [HumanResources].[Employee] e\r\n inner join Person.Person p on e.BusinessEntityID=p.BusinessEntityID\r\n" +
                     " inner join HumanResources.EmployeeDepartmentHistory dh on e.BusinessEntityID=dh.BusinessEntityID\r\n" +
                     " inner join HumanResources.Department d on d.DepartmentID=dh.DepartmentID " +
                     (departmentId != null ? "where d.DepartmentID=@DepartmentId" : "");

        IEnumerable<T> result;

        if (departmentId != null)
            result = await connection.QueryAsync<T>(sql, new { DepartmentId = departmentId });
        else
            result = await connection.QueryAsync<T>(sql);

        return result;
    }

    public async Task Update(Employee employee)
    {
        using IDbConnection connection = new SqlConnection(_connectionString);
        string updateSql = "update Person.Person set FirstName=@firstName, LastName=@lastName where BusinessEntityId=@businessEntityId";

        var employeeEntity = _mapper.Map<EmployeeEntity>(employee);

        await connection.ExecuteAsync(updateSql,
            new
            {
                firstName = employeeEntity.FirstName, lastName = employeeEntity.LastName, businessEntityId = employeeEntity.BusinessEntityId
            });
    }
}