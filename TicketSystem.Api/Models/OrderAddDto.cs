namespace TicketSystem.Api.Models
{
    public class OrderAddDto
    {
        public int TrainId { get; set; }
        public int StartTerminalId { get; set; }
        public int EndTerminalId { get; set; }
        public double Price { get; set; }
    }
}
