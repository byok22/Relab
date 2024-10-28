using System.Text.Json;
using Domain.Models.Employees;
using Domain.Models.Generics;
using Domain.Services;
using Shared.Response;

namespace Presentation.Services
{
    public class EmployeesService : IEmployeesService
    {
        private readonly IMsgService _msgService;
        private readonly ILogger<EmployeesService> _logger;

        public EmployeesService(IMsgService msgService, ILogger<EmployeesService> logger)
        {
            _msgService = msgService;
            _logger = logger;
        }

        public async Task<GenericResponse> AddEmployee(Employee equipment)
        {
            _logger.LogInformation("Sending request on topic AddEmployee");            
            try
            {
                var message = JsonSerializer.Serialize( equipment );
                var response = await _msgService.RequestAsync("AddEmployee", message);
                if (string.IsNullOrEmpty(response))
                {
                    _logger.LogWarning("Received empty response from AddEmployee Request");
                    return new GenericResponse(){
                        IsSuccessful= false,
                        Message = "Received empty response from AddEmployee Request"                        
                    };
                }
                _logger.LogInformation("Deserializing response from AddEmployee Request");
                return JsonSerializer.Deserialize<GenericResponse>(response) ?? new GenericResponse();
            }
            catch (JsonException ex)
            {
                _logger.LogError(ex, "Error al deserializar la respuesta del microservicio.");
                throw new InvalidOperationException("Error al deserializar la respuesta del microservicio.", ex);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error de respuesta del microservicio.");
                throw new InvalidOperationException("Error respuesta del microservicio.", ex);
            }
        }

       
        public async Task<List<Employee>> GetAllEmployees()
        {
             _logger.LogInformation("Sending request on topic GetAllEmployees");            
            try
            {
                var response = await _msgService.RequestAsync("GetAllEmployees");

                if (string.IsNullOrEmpty(response))
                {
                    _logger.LogWarning("Received empty response from GetAllEmployees");
                    return new List<Employee>();
                }
                _logger.LogInformation("Deserializing response from GetAllEmployees");
                return JsonSerializer.Deserialize<List<Employee>>(response);
            }
            catch (JsonException ex)
            {
                _logger.LogError(ex, "Error al deserializar la respuesta del microservicio.");
                throw new InvalidOperationException("Error al deserializar la respuesta del microservicio.", ex);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error de respuesta del microservicio.");
                throw new InvalidOperationException("Error respuesta del microservicio.", ex);
            }
        }

             public async Task<Employee> GetEmployeeByEmployeeNumber(string employeeNumber)
        {
             _logger.LogInformation("Sending request on topic GetEmployeeByEmployeeNumber");            
            try
            {
                var message = JsonSerializer.Serialize(employeeNumber);
                var response = await _msgService.RequestAsync("GetEmployeeByEmployeeNumber", message);

                if (string.IsNullOrEmpty(response))
                {
                    _logger.LogWarning("Received empty response from GetEmployeeByEmployeeNumber");
                    return new Employee();
                }
                _logger.LogInformation("Deserializing response from GetEmployeeByEmployeeNumber");
                return JsonSerializer.Deserialize<Employee>(response);
            }
            catch (JsonException ex)
            {
                _logger.LogError(ex, "Error al deserializar la respuesta del microservicio.");
                throw new InvalidOperationException("Error al deserializar la respuesta del microservicio.", ex);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error de respuesta del microservicio.");
                throw new InvalidOperationException("Error respuesta del microservicio.", ex);
            }
        }

        public Task<List<DropDown>> GetEmployeeStatus()
        {
            //GetEmployeeStatus
            _logger.LogInformation("Sending request on topic GetEmployeeStatus");
            try
            {
                var response = _msgService.RequestAsync("GetEmployeeStatus").Result;
                if (string.IsNullOrEmpty(response))
                {
                    _logger.LogWarning("Received empty response from GetEmployeeStatus Request");
                    return Task.FromResult(new List<DropDown>());
                }
                _logger.LogInformation("Deserializing response from GetEmployeeStatus Request");
                return Task.FromResult(JsonSerializer.Deserialize<List<DropDown>>(response));
            }
            catch (JsonException ex)
            {
                _logger.LogError(ex, "Error al deserializar la respuesta del microservicio.");
                throw new InvalidOperationException("Error al deserializar la respuesta del microservicio.", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error de respuesta del microservicio.");
                throw new InvalidOperationException("Error respuesta del microservicio.", ex);
            }
        }

        public async Task<GenericResponse> UpdateEmployee(Employee equipment)
        {
             _logger.LogInformation("Sending request on topic UpdateTest");            
            try
            {
                var message = JsonSerializer.Serialize( equipment );
                var response = await _msgService.RequestAsync("PatchEmployee", message);
                if (string.IsNullOrEmpty(response))
                {
                    _logger.LogWarning("Received empty response from UpdateEmployee Request");
                    return new GenericResponse(){
                        IsSuccessful= false,
                        Message = "Received empty response from UpdateEmployee Request"                        
                    };
                }
                _logger.LogInformation("Deserializing response from UpdateEmployee Request");
                return JsonSerializer.Deserialize<GenericResponse>(response) ?? new GenericResponse();
            }
            catch (JsonException ex)
            {
                _logger.LogError(ex, "Error al deserializar la respuesta del microservicio.");
                throw new InvalidOperationException("Error al deserializar la respuesta del microservicio.", ex);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error de respuesta del microservicio.");
                throw new InvalidOperationException("Error respuesta del microservicio.", ex);
            }
        }

        public async Task<GenericResponse> RemoveEmployee(int id)
        {
             _logger.LogInformation("Sending request on topic RemoveEmployee");            
            try
            {
                var message = JsonSerializer.Serialize(id);
                var response = await _msgService.RequestAsync("RemoveEmployee", message);
                if (string.IsNullOrEmpty(response))
                {
                    _logger.LogWarning("Received empty response from RemoveEmployee Request");
                    return new GenericResponse(){
                        IsSuccessful= false,
                        Message = "Received empty response from RemoveEmployee Request"                        
                    };
                }
                _logger.LogInformation("Deserializing response from RemoveEmployee Request");
                return JsonSerializer.Deserialize<GenericResponse>(response) ?? new GenericResponse();
            }
            catch (JsonException ex)
            {
                _logger.LogError(ex, "Error al deserializar la respuesta del microservicio.");
                throw new InvalidOperationException("Error al deserializar la respuesta del microservicio.", ex);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error de respuesta del microservicio.");
                throw new InvalidOperationException("Error respuesta del microservicio.", ex);
            }
        }

        public async Task<List<Employee>> GetEmployeesByType(EmployeeTypeEnum employeeType)
        {
           _logger.LogInformation("Sending request on topic GetEmployeesByType");
            try
            {
                var message = JsonSerializer.Serialize(employeeType);
                var response = _msgService.RequestAsync("GetEmployeesByType", message).Result;
                if (string.IsNullOrEmpty(response))
                {
                    _logger.LogWarning("Received empty response from GetEmployeesByType Request");
                    return new List<Employee>();
                    
                    
                }
                _logger.LogInformation("Deserializing response from GetEmployeesByType Request");
                return JsonSerializer.Deserialize<List<Employee>>(response);

                
              



        } catch (JsonException ex)
            {
                _logger.LogError(ex, "Error al deserializar la respuesta del microservicio.");
                throw new InvalidOperationException("Error al deserializar la respuesta del microservicio.", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error de respuesta del microservicio.");
                throw new InvalidOperationException("Error respuesta del microservicio.", ex);
            }
        }
    }
}