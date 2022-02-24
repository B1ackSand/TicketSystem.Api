namespace TicketSystem.Api.Models
{
    public class StationOutputDto
    {
        public Guid Id { get; set; }
        public string StationName { get; set; }
        public bool IsTerminal { get; set; }
    }
}
