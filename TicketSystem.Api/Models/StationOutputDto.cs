﻿namespace TicketSystem.Api.Models
{
    public class StationOutputDto
    {
        public Guid StationId { get; set; }
        public string StationName { get; set; }
        public bool IsTerminal { get; set; }
    }
}
