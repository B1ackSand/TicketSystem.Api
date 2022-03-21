namespace TicketSystem.Api.Models
{
    public class OrderOutputDto
    {
        public Guid OrderId { get; set; }
        public Guid TrainId { get; set; }
        public Guid StartTerminalId { get; set; }
        public string StartTerminal { get; set; }
        public Guid EndTerminalId { get; set; }
        public string EndTerminal { get; set; }
        public string TrainName { get; set; }
        public DateTime CreatedDate { get; set; }
        public double price { get; set; }
    }
}
