namespace TicketSystem.Api.Entities
{
    public class Order
    {
        public Guid OrderId { get; set; }
        public int BookerId { get; set; }
        public int TrainId { get; set; }
        public int StartTerminalId { get; set; }
        public int EndTerminalId { get; set; }
        public string StartTerminal { get; set; }
        public string EndTerminal { get; set; }
        public string TrainName { get; set; }
        public DateTime CreatedDate { get; set; }
        public double price { get; set; }
    }
}
