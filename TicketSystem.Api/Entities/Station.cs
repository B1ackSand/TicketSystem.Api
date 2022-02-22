namespace TicketSystem.Api.Entities;

public class Station
{
    public Guid Id { get; set; }
    public string StationName { get; set; }
    public bool IsTerminal { get; set; }
}