using Routine.Api.DtoParameters;
using Routine.Api.Entities;

namespace Routine.Api.Services
{
    //仓储接口，负责放置一些重复性的代码如CURD操作
    public interface ICompanyRepository
    {
        Task<IEnumerable<Company>> GetCompaniesAsync(CompanyDtoParameters? parameters);
        Task<Company> GetCompaniesAsync(Guid companyId);
        Task<IEnumerable<Company>> GetCompaniesAsync(IEnumerable<Guid> companyIds);
        void AddCompany(Company company);
        void DeleteCompany(Company company);
        void UpdateCompany(Company company);
        Task<bool> CompanyExistsAsync(Guid companyId);


        Task<IEnumerable<Employee>> GetEmployeesAsync(Guid companyId, string? genderDisplay, string? q);
        Task<Employee> GetEmployeeAsync(Guid companyId, Guid employeeId);
        void AddEmployee(Guid companyId, Employee employee);
        void DeleteEmployee(Employee employee);
        void UpdateEmployee(Employee employee);
        Task<bool> SaveAsync();
    }
}
