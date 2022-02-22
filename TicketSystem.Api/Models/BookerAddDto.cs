using Routine.Api.Entities;

namespace TicketSystem.Api.Models
{
    public class BookerAddDto
    {
        public string BookerWx { get; set; }
        public string UserName { get; set; }
        public string BookerPwd { get; set; }
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        public Gender Gender { get; set; }
        public string? PhoneNum { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
