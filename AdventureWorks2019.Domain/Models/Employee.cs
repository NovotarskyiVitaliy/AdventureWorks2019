namespace AdventureWorks2019.Domain.Models;

public class Employee
{
    public int BusinessEntityId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? Name { get; set; }
    public string? GroupName { get; set; }
    public int DepartmentId { get; set; }
}