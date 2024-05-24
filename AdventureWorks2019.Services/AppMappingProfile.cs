using AdventureWorks2019.DataAccess.Entities;
using AdventureWorks2019.Domain.Models;
using AutoMapper;

namespace AdventureWorks2019.Application;

public class AppMappingProfile : Profile
{
    public AppMappingProfile()
    {
        CreateMap<EmployeeEntity, Employee>()
            .ForMember(dest => dest.FirstName,x => x.MapFrom(y =>$"{y.FirstName}"));
    }
}