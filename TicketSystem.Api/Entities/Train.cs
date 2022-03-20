using Routine.Api.Entities;
namespace TicketSystem.Api.Entities;

public class Train
{
    public Guid TrainId { get; set; }
    public Guid LineId { get; set; }
    public string TrainName { get; set; }
    public string TypeOfTrain { get; set; }
    public Line Line { get; set; }
    public TimeOnly Time { get; set; }
}