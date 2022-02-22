using Routine.Api.Entities;
namespace TicketSystem.Api.Entities;

public class Train
{
    public Guid Id { get; set; }
    public string TrainName { get; set; }
    public string TypeOfTrain { get; set; }
}