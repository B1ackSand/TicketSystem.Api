namespace TicketSystem.Api.Models
{
    public class OrderDto
    {
        public Guid OrderId { get; set; }
        public int BookerId { get; set; }
        public Guid TrainId { get; set; }
        public Guid StartTerminalId { get; set; }
        public Guid EndTerminalId { get; set; }
        public DateTime CreatedDate { get; set; }
        public double price { get; set; }
    }
}
