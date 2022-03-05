using Routine.Api.Entities;

namespace TicketSystem.Api.Models;

public class BookerOutputDto
{
    public Guid bookerId { get; set; }
    public string UserName { get; set; }
    public string Name { get; set; }
    public Gender Gender { get; set; }
    public string? PhoneNum { get; set; }
    public DateTime TimeOfRegister { get; set; }
    public int Age { get; set; }
}