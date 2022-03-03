namespace TicketSystem.Api.Models
{
    public class TrainUpdateDto:TrainAddDto
    {
        public Guid LineId { get; set; }
    }
}
