using TicketSystem.Api.Entities;

namespace TicketSystem.Api.Models
{
    public class TrainDto
    {
        public Guid TrainId { get; set; }
        public Guid LineId { get; set; }
        public string TrainName { get; set; }
        public string TypeOfTrain { get; set; }
        public TimeOnly Time { get; set; }
    }
}
