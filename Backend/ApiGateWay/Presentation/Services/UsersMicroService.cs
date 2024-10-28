using System.Text.Json;
using Domain.Models;
using Domain.Services;
using Shared.Dtos;


namespace Presentation.Services
{
    public class UsersMicroService : IUsersMicroService
    {
        private readonly IMsgService _msgService;
        private readonly ILogger<UsersMicroService> _logger;
        public UsersMicroService(IMsgService msgService, ILogger<UsersMicroService> logger)
        {
            _msgService = msgService;
            _logger = logger;
        }

        public async Task<UserDto> AddUser(UserDto user)
        {
            _logger.LogInformation("Sending request on topic addUser");            
            try
            {
                var message = JsonSerializer.Serialize(new { User = user });
                var response = await _msgService.RequestAsync("addUser", message);

                if (string.IsNullOrEmpty(response))
                {
                    _logger.LogWarning("Received empty response from addUser Request");
                    return new UserDto();
                }
                _logger.LogInformation("Deserializing response from addUser Request");
                return JsonSerializer.Deserialize<UserDto>(response)??new UserDto();
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

        public async Task<List<UserDto>> GetAllUsers()
        {
            _logger.LogInformation("Sending request on topic getAllUsers");            
            try
            {
                var response = await _msgService.RequestAsync("getAllUsers");

                if (string.IsNullOrEmpty(response))
                {
                    _logger.LogWarning("Received empty response from getAllUsersRequest");
                    return new List<UserDto>();
                }
                _logger.LogInformation("Deserializing response from getAllUsersRequest");
                return JsonSerializer.Deserialize<List<UserDto>>(response);
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

        public async Task<UserDto> GetUserByEmployeeAccount(string employeeAccount)
        {
            _logger.LogInformation("Sending request on topic getUserByEmployeeAccount");            
            try
            {
                var message = JsonSerializer.Serialize(new { EmployeeAccount = employeeAccount });
                var response = await _msgService.RequestAsync("getUserByEmployeeAccount", message);

                if (string.IsNullOrEmpty(response))
                {
                    _logger.LogWarning("Received empty response from getUserByEmployeeAccount Request");
                    return new UserDto();
                }
                _logger.LogInformation("Deserializing response from getUserByEmployeeAccount Request");
                return JsonSerializer.Deserialize<UserDto>(response);
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
    
        public async Task<UserDto>  GetUserByID(int id){

            _logger.LogInformation("Sending request on topic getUserByEmployeeAccount");            
            try
            {
                var message = JsonSerializer.Serialize(new { Id = id });
                var response = await _msgService.RequestAsync("getUserById", message);

                if (string.IsNullOrEmpty(response))
                {
                    _logger.LogWarning("Received empty response from getUserById Request");
                    return new UserDto();
                }
                _logger.LogInformation("Deserializing response from getUserById Request");
                return JsonSerializer.Deserialize<UserDto>(response);
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
    
        public async Task<UserDto> GetUserInfoLdapByEmployeeAccount(string employeeAccount)
        {
            _logger.LogInformation("Sending request on topic getUserInfoLdapByEmployeeAccount");            
            try
            {
                var message = JsonSerializer.Serialize(new { EmployeeAccount = employeeAccount });
                var response = await _msgService.RequestAsync("getUserInfoLdapByEmployeeAccount", message);

                if (string.IsNullOrEmpty(response))
                {
                    _logger.LogWarning("Received empty response from getUserInfoLdapByEmployeeAccount Request");
                    return new UserDto();
                }
                _logger.LogInformation("Deserializing response from getUserInfoLdapByEmployeeAccount Request");
                return JsonSerializer.Deserialize<UserDto>(response);
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
    
         public async Task<List<UserDto>> GetUsersByType(string type)
         {
            _logger.LogInformation("Sending request on topic getUsersByType");            
            try
            {
                var message = JsonSerializer.Serialize(new { Type = type });
                var response = await _msgService.RequestAsync("getUsersByType", message);

                if (string.IsNullOrEmpty(response))
                {
                    _logger.LogWarning("Received empty response from getUsersByType Request");
                    return new List<UserDto>();
                }
                _logger.LogInformation("Deserializing response from getUsersByType Request");
                return JsonSerializer.Deserialize<List<UserDto>>(response);
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

        public async Task<UserDto> UpdateUser(UserDto user)
        {
             _logger.LogInformation("Sending request on topic updateUser");            
            try
            {
                var message = JsonSerializer.Serialize(new { User = user });
                var response = await _msgService.RequestAsync("updateUser", message);

                if (string.IsNullOrEmpty(response))
                {
                    _logger.LogWarning("Received empty response from updateUser Request");
                    return new UserDto();
                }
                _logger.LogInformation("Deserializing response from updateUser Request");
                return JsonSerializer.Deserialize<UserDto>(response)??new UserDto();
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
   
        public async Task<List<UserProfile>> GetUserProfiles(){

             _logger.LogInformation("Sending request on topic GetUserProfiles");            
            try
            {
                var response = await _msgService.RequestAsync("getProfiles");

                if (string.IsNullOrEmpty(response))
                {
                    _logger.LogWarning("Received empty response from GetUserProfiles");
                    return new List<UserProfile>();
                }
                _logger.LogInformation("Deserializing response from GetUserProfiles");
                return JsonSerializer.Deserialize<List<UserProfile>>(response);
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
    }
}
