namespace TicketSystem.Api.Entities;

public class Station
{
    public int StationId { get; set; }
    public string StationName { get; set; }
    public bool IsTerminal { get; set; }
    public ICollection<Line> Lines { get; set; }
}