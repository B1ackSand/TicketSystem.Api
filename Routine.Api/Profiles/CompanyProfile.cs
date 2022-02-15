using AutoMapper;
using Routine.Api.Entities;
using Routine.Api.Models;

namespace Routine.Api.Profiles
{
    //AutoMapper models映射文件
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
