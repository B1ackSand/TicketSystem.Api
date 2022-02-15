using AutoMapper;
using Routine.Api.Entities;
using Routine.Api.Models;

namespace Routine.Api.Profiles
{
    //AutoMapper models映射文件
    //Dto和entity的映射关系，区分dto中的成员和entity的对应关系
    public class CompanyProfile: Profile
    {
        public CompanyProfile()
        {
            CreateMap<Company, CompanyDto>()
                .ForMember(dest =>dest.CompanyName,
                    opt => opt.MapFrom(src => src.Name));

            CreateMap<CompanyAddDto, Company>();
        }
    }
}
