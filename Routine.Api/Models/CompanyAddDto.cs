using System.ComponentModel.DataAnnotations; //输入验证

namespace Routine.Api.Models
{
    //面向外部的models
    public class CompanyAddDto
    {
        //0是属性名称，1是当前属性从左往右的第一个参数
        [Display(Name = "公司名")]
        [Required(ErrorMessage = "{0}这个字段是必填的！")]
        [MaxLength(100, ErrorMessage = "{0}的最大长度不可以超过{1}")]
        public string Name { get; set; }

        [Display(Name = "简介")]
        [StringLength(500,MinimumLength = 1, ErrorMessage = "{0}的范围是从{2}到{1}")]
        public string? Introduction { get; set; }
        // 声明的同时实例化，且可以同时创建父子资源，无需映射因为命名相同
        public ICollection<EmployeeAddDto>? Employees { get; set; } = new List<EmployeeAddDto>();
    }
}
