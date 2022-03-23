using AutoMapper;
using TicketSystem.Api.Entities;
using TicketSystem.Api.Models;

namespace TicketSystem.Api.Profiles;

public class DistanceProfile : Profile
{
    public DistanceProfile()
    {
        CreateMap<Distance, DistanceOutputDto>()
            .ForMember(dest => dest.Distance,
                opt => opt.MapFrom(src => src.StationDistance));
    }
}