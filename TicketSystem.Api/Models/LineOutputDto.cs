﻿namespace TicketSystem.Api.Models
{
    public class LineOutputDto
    {
        public int LineId { get; set; }
        public string StartTerminal { get; set; }
        public string EndTerminal { get; set; }
        public string StopStation { get; set; }
        public string? TrainName { get; set; }
    }
}
