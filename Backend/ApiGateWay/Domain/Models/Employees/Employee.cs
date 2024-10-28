namespace Domain.Models.Employees
{
    public class Employee
    {
        public int Id { get; set; }
        public string EmployeeNumber { get; set;} = string.Empty;
        public string Name { get; set;} = string.Empty;
        public EmployeeTypeEnum EmployeeType{ get; set;}
        
    }
}