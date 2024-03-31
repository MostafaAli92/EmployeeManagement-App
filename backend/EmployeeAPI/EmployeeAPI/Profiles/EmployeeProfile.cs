using AutoMapper;
using EmployeeAPI.Dtos.Employee;
using EmployeeAPI.Dtos.User;
using EmployeeManagement.Data.Models;
using EmployeeManagement.Domain.Entities;

namespace EmployeeAPI.Profiles
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile() 
        {
            CreateMap<Employee, EmployeeReadDto>().ForMember(dist => dist.Department, act => act.MapFrom(src => src.DepartmentName));

            CreateMap<EmployeeCreateDto, Employee>().ForMember( dist => dist.DepartmentName, act => act.MapFrom( src => src.Department ) );

            CreateMap<EmployeeUpdateDto, Employee>().ForMember( dist => dist.DepartmentName,act => act.MapFrom(src => src.Department) );

            CreateMap<Employee, EmployeeUpdateDto>().ForMember( dist => dist.Department, act => act.MapFrom( src => src.DepartmentName ) ); 

            CreateMap<ApplicationUser,UserReadDto>();
        }
    }
}
