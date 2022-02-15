namespace Routine.Api.Models
{
    //面向外部的models
    public class CompanyAddDto
    {
        public string Name { get; set; }
        public string Introduction { get; set; }
        // 声明的同时实例化，且可以同时创建父子资源，无需映射因为命名相同
        public ICollection<EmployeeAddDto>? Employees { get; set; } = new List<EmployeeAddDto>();
    }
}
