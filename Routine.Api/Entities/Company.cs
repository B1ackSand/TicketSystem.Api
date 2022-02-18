namespace Routine.Api.Entities
{
    //实体models
    public class Company
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string? Introduction { get; set; }

        public ICollection<Employee> Employees { get; set; }
    }
}
