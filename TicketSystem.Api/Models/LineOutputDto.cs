namespace TicketSystem.Api.Models
{
    public class LineOutputDto
    {
        public string StartTerminal { get; set; }
        public string EndTerminal { get; set; }
        public string StopStation { get; set; }
        public string? TrainName { get; set; }
    }
}
