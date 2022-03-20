namespace TicketSystem.Api.Entities
{
    public class Order
    {
        public Guid OrderId { get; set; }
        public int BookerId { get; set; }
        public Guid TrainId { get; set; }
        public Guid StartTerminalId { get; set; }
        public Guid EndTerminalId { get; set; }
        public string StartTerminal { get; set; }
        public string EndTerminal { get; set; }
        public string TrainName { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
