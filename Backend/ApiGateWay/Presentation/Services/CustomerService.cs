using System.Text.Json;
using Domain.Services;
using Shared.Dtos;
using Shared.Response;

namespace Presentation.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IMsgService _msgService;
        private readonly ILogger<CustomerService> _logger;

        public CustomerService(IMsgService msgService, ILogger<CustomerService> logger)
        {
            _msgService = msgService;
            _logger = logger;
        }

        public async Task<GenericResponse> AddCustomer(CustomerDto customerDto)
        {
            _logger.LogInformation("Sending request on topic AddCustomer");
            try
            {
                var message = JsonSerializer.Serialize(customerDto);
                var response = await _msgService.RequestAsync("AddCustomer", message);
                if (string.IsNullOrEmpty(response))
                {
                    _logger.LogWarning("Received empty response from AddCustomer Request");
                    return new GenericResponse
                    {
                        IsSuccessful = false,
                        Message = "Received empty response from AddCustomer Request"
                    };
                }
                _logger.LogInformation("Deserializing response from AddCustomer Request");
                return JsonSerializer.Deserialize<GenericResponse>(response) ?? new GenericResponse();
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

        public async Task<List<CustomerDto>> GetAllCustomer()
        {
            _logger.LogInformation("Sending request on topic GetAllCustomers");
            try
            {
                var response = await _msgService.RequestAsync("GetAllCustomers");

                if (string.IsNullOrEmpty(response))
                {
                    _logger.LogWarning("Received empty response from GetAllCustomers");
                    return new List<CustomerDto>();
                }
                _logger.LogInformation("Deserializing response from GetAllCustomers");
                return JsonSerializer.Deserialize<List<CustomerDto>>(response);
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

        public async Task<CustomerDto> GetCustomerByCustomerId(string customerId)
        {
            _logger.LogInformation("Sending request on topic GetCustomerByCustomerId");
            try
            {
            var message = JsonSerializer.Serialize(customerId);
            var response = await _msgService.RequestAsync("GetCustomerByCustomerId", message);

            if (string.IsNullOrEmpty(response))
            {
                _logger.LogWarning("Received empty response from GetCustomerByCustomerId");
                return new CustomerDto();
            }
            _logger.LogInformation("Deserializing response from GetCustomerByCustomerId");
            return JsonSerializer.Deserialize<CustomerDto>(response);
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

        public async Task<CustomerDto> GetCustomerById(int id)
        {
            _logger.LogInformation("Sending request on topic GetCustomerById");
            try
            {
                var message = JsonSerializer.Serialize(id);
                var response = await _msgService.RequestAsync("GetCustomerById", message);

                if (string.IsNullOrEmpty(response))
                {
                    _logger.LogWarning("Received empty response from GetCustomerById");
                    return new CustomerDto();
                }
                _logger.LogInformation("Deserializing response from GetCustomerById");
                return JsonSerializer.Deserialize<CustomerDto>(response);
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

        public async Task<GenericResponse> PatchCustomer(CustomerDto customerDto)
        {
            _logger.LogInformation("Sending request on topic PatchCustomer");
            try
            {
                var message = JsonSerializer.Serialize(customerDto);
                var response = await _msgService.RequestAsync("PatchCustomer", message);
                if (string.IsNullOrEmpty(response))
                {
                    _logger.LogWarning("Received empty response from PatchCustomer Request");
                    return new GenericResponse
                    {
                        IsSuccessful = false,
                        Message = "Received empty response from PatchCustomer Request"
                    };
                }
                _logger.LogInformation("Deserializing response from PatchCustomer Request");
                return JsonSerializer.Deserialize<GenericResponse>(response) ?? new GenericResponse();
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
    }
}