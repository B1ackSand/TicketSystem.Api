﻿namespace TicketSystem.Api.Models
{
    public class OrderDto
    {
        public Guid OrderId { get; set; }
        public int BookerId { get; set; }
        public int TrainId { get; set; }
        public int StartTerminalId { get; set; }
        public int EndTerminalId { get; set; }
        public DateTime CreatedDate { get; set; }
        public double Price { get; set; }
        public string DateBook { get; set; }
    }
}
