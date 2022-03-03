namespace TicketSystem.Api.Models
{
    public class OrderAddDto
    {
        public Guid TrainId { get; set; }
        public Guid StartTerminalId { get; set; }
        public Guid EndTerminalId { get; set; }
    }
}
