using AutoMapper;
using Routine.Api.Entities;
using Routine.Api.Models;

namespace Routine.Api.Profiles
{
    //Dto和entity的映射关系，区分dto中的成员和entity的对应关系
    public class EmployeeProfile: Profile
    {
        public EmployeeProfile()
        {
            CreateMap<Employee, EmployeeDto>()
                .ForMember(dest => dest.Name,
                    opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
                .ForMember(dest => dest.GenderDisplay,
                    opt => opt.MapFrom(src => src.Gender.ToString()))
                .ForMember(dest => dest.Age,
                    opt => opt.MapFrom(src => DateTime.Now.Year - src.DateOfBirth.Year));

            CreateMap<EmployeeAddDto, Employee>();
        }
    }
}
