using AutoMapper;
using TicketSystem.Api.Entities;
using TicketSystem.Api.Models;

namespace TicketSystem.Api.Profiles
{
    public class StationProfile: Profile
    {
        public StationProfile()
        {
            CreateMap<Station, StationOutputDto>();
            CreateMap<StationAddDto, Station>();
        }
    }
}
