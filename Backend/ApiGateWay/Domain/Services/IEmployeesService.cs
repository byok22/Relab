using Domain.Models.Employees;
using Shared.Response;

namespace Domain.Services
{
    public interface IEmployeesService
    {
       


        /*
          Task<GenericResponse> AddEquipment(Equipment testDto);
        Task<List<Equipment>> GetAllEquipments();
        Task<Equipment> GetEquipmentById(int id);
       
        Task<GenericResponse> PatchEquipment(Equipment testDto);      
        Task<List<DropDown>> GetEquipmentsStatus();         
        */

        Task<GenericResponse> AddEmployee(Employee employeeEntity);
        Task<GenericResponse> RemoveEmployee(int employeeId);
        Task<GenericResponse> UpdateEmployee(Employee employeeEntity);
        Task<Employee> GetEmployeeByEmployeeNumber(string employeeNumber);
        Task<List<Employee>> GetAllEmployees();       
        Task<List<Employee>> GetEmployeesByType(EmployeeTypeEnum employeeType);

        
    }
}