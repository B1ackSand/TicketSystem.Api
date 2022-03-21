namespace TicketSystem.Api.Entities
{
    public class Line
    {
        public int LineId { get; set; }
        public string StartTerminal { get; set; }
        public string EndTerminal { get; set; }
        public string StopStation { get; set; }
        public string? TrainName { get; set; }
        public ICollection<Train> Trains { get; set; }
    }
}
