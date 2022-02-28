namespace TicketSystem.Api.Entities
{
    public class Line
    {
        public Guid Id { get; set; }
        //public string LineName { get; set; }
        public string StartTerminal { get; set; }
        public string EndTerminal { get; set; }
        public string StopStation { get; set; }
    }
}
