using Domain.Models.Employees;

namespace Domain.Repositories
{
    public interface IEmployeeRepository: IGenericRepository<Employee>
    {
        Task<Employee> GetByEmployeeNumberAsync(string employeeNumber);
        Task<List<Employee>> GetByEmployeebyType(EmployeeTypeEnum employeeType);
        
    }
}