using AutoMapper;
using TicketSystem.Api.Entities;
using TicketSystem.Api.Models;

namespace TicketSystem.Api.Profiles
{
    //Dto和entity的映射关系，区分dto中的成员和entity的对应关系
    public class BookerProfile : Profile
    {
        public BookerProfile()
        {
            CreateMap<Booker, BookerOutputDto>()
                .ForMember(dest => dest.Name,
                    opt => opt.MapFrom(src => $"{src.FirstName}{src.LastName}"))
                .ForMember(dest => dest.Age,
                    opt => opt.MapFrom(src => DateTime.Now.Year - src.DateOfBirth.Year));

            CreateMap<BookerAddDto, Booker>();
            CreateMap<BookerLoginDto, Booker>();
            CreateMap<Booker, BookerDto>();
        }
    }
}
