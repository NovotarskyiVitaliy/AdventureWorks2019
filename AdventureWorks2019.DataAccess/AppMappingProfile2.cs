using AdventureWorks2019.DataAccess.Entities;
using AdventureWorks2019.Domain.Models;
using AutoMapper;

namespace AdventureWorks2019.DataAccess;

public class AppMappingProfile2 : Profile
{
    public AppMappingProfile2()
    {
        CreateMap<Employee, EmployeeEntity>();
    }
}