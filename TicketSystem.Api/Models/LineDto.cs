namespace TicketSystem.Api.Models
{
    public class LineDto
    {
        public Guid LineId { get; set; }
        public string StartTerminal { get; set; }
        public string EndTerminal { get; set; }
        public string StopStation { get; set; }
    }
}
