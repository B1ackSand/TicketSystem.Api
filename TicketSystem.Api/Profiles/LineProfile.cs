using AutoMapper;
using TicketSystem.Api.Entities;
using TicketSystem.Api.Models;

namespace TicketSystem.Api.Profiles
{
    public class LineProfile: Profile
    {
        public LineProfile()
        {
            CreateMap<Line, LineOutputDto>();
            CreateMap<Line, LineDto>();
            CreateMap<LineAddDto, Line>();
        }
        
    }
}
