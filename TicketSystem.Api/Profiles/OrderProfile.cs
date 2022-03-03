using AutoMapper;
using TicketSystem.Api.Entities;
using TicketSystem.Api.Models;

namespace TicketSystem.Api.Profiles
{
    public class OrderProfile: Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, OrderDto>();
            CreateMap<OrderAddDto,Order>(); 
        }
    }
}
