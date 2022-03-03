using AutoMapper;
using TicketSystem.Api.Entities;
using TicketSystem.Api.Models;

namespace TicketSystem.Api.Profiles
{
    //Dto和entity的映射关系，区分dto中的成员和entity的对应关系
    public class TrainProfile: Profile
    {
        public TrainProfile()
        {
            CreateMap<Train, TrainOutputDto>();
            CreateMap<Train, TrainDto>();
            CreateMap<TrainAddDto, Train>();
            CreateMap<TrainUpdateDto, Train>();
        }
    }
}
