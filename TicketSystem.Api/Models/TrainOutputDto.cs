﻿namespace TicketSystem.Api.Models
{
    public class TrainOutputDto
    {
        public int TrainId { get; set; }
        public int LineId { get; set; }
        public string TrainName { get; set; }
        public string TypeOfTrain { get; set; }
        public TimeOnly Time { get; set; }
    }
}
