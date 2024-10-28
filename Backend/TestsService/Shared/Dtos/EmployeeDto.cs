using Domain.Models.Employees;

namespace Shared.Dtos
{
    public class EmployeeDto
    {
        
        public int Id { get; set; }
        public string EmployeeNumber { get; set;}= string.Empty;
        public string Name { get; set;} = string.Empty;
        public EmployeeTypeEnum EmployeeType{ get; set;}
    }
}