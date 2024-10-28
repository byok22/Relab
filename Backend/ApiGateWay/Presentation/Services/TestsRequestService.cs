using System.Text.Json;
using Domain.Enums;
using Domain.Models.TestRequests;
using Domain.Services;
using Shared.Dtos;
using Shared.Request;
using Shared.Response;

namespace Presentation.Services
{
    public class TestsRequestService : ITestRequestService
    {
        private readonly IMsgService _msgService;
        private readonly ILogger<TestsMicroServices> _logger;

        public TestsRequestService(IMsgService msgService, ILogger<TestsMicroServices> logger)
        {
            _msgService = msgService;
            _logger = logger;
        }

        public async Task<GenericResponse> AddTestRequest(TestRequestDto testRequestDto)
        {
            _logger.LogInformation("Sending request on topic AddTest");            
            try
            {
                var message = JsonSerializer.Serialize( testRequestDto );
                var response = await _msgService.RequestAsync("AddTestRequest", message);
                if (string.IsNullOrEmpty(response))
                {
                    _logger.LogWarning("Received empty response from AddTestRequest Request");
                    return new GenericResponse(){
                        IsSuccessful= false,
                        Message = "Received empty response from AddTestRequest Request"                        
                    };
                }
                _logger.LogInformation("Deserializing response from AddTestRequest Request");
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

        public async Task<GenericResponse> ApproveOrRejectTestRequest(int id, ChangeStatusTestRequest changeStatusTestRequest)
        {
            _logger.LogInformation("Sending request on topic ApproveOrRejectTestRequest");            
            try
            {
                var message = JsonSerializer.Serialize(new { id,  changeStatusTestRequest });
                var response = await _msgService.RequestAsync("ApproveOrRejectTestRequest", message);
                if (string.IsNullOrEmpty(response))
                {
                    _logger.LogWarning("Received empty response from ApproveOrRejectTestRequest Request");
                    return new GenericResponse(){
                        IsSuccessful= false,
                        Message = "Received empty response from ApproveOrRejectTestRequest Request"                        
                    };
                }
                _logger.LogInformation("Deserializing response from ApproveOrRejectTestRequest Request");
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

        public async Task<GenericResponse> ChangeStatusTestRequest(int id, ChangeStatusTestRequest changeStatusTestRequest)
        {
               _logger.LogInformation("Sending request on topic ChangeStatusTestRequest");            
            try
            {
                var message = JsonSerializer.Serialize(new { id,  changeStatusTestRequest });
                var response = await _msgService.RequestAsync("ChangeStatusTestRequest", message);
                if (string.IsNullOrEmpty(response))
                {
                    _logger.LogWarning("Received empty response from ChangeStatusTestRequest Request");
                    return new GenericResponse(){
                        IsSuccessful= false,
                        Message = "Received empty response from ChangeStatusTestRequest Request"                        
                    };
                }
                _logger.LogInformation("Deserializing response from ChangeStatusTestRequest Request");
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

        public async Task<List<TestRequestDto>> GetAllTestRequests()
        {
             _logger.LogInformation("Sending request on topic GetAllTestRequests");            
            try
            {
                var response = await _msgService.RequestAsync("GetAllTestRequests");

                if (string.IsNullOrEmpty(response))
                {
                    _logger.LogWarning("Received empty response from GetAllTestRequests");
                    return new List<TestRequestDto>();
                }
                _logger.LogInformation("Deserializing response from GetAllTestRequests");
                return JsonSerializer.Deserialize<List<TestRequestDto>>(response);
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

        public async Task<List<TestRequestDto>> GetAllTestRequestsByStatus(TestRequestsStatus testRequestsStatus)
        {
            _logger.LogInformation("Sending request on topic GetAllTestRequestsByStatus");            
            try
            {
                var message = JsonSerializer.Serialize(new TestRequestsRequest(){ Status = testRequestsStatus });
                var response = await _msgService.RequestAsync("GetAllTestRequestsByStatus", message);

                if (string.IsNullOrEmpty(response))
                {
                    _logger.LogWarning("Received empty response from GetAllTestRequestsByStatus Request");
                    return new List<TestRequestDto>();
                }
                _logger.LogInformation("Deserializing response from GetAllTestRequestsByStatus Request");
                return JsonSerializer.Deserialize<List<TestRequestDto>>(response);
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

        public async Task<TestRequestDto> GetTestRequestsById(int id)
        {
             _logger.LogInformation("Sending request on topic GetTestRequestById");            
            try
            {
                var message = JsonSerializer.Serialize(id);
                var response = await _msgService.RequestAsync("GetTestRequestById", message);

                if (string.IsNullOrEmpty(response))
                {
                    _logger.LogWarning("Received empty response from GetTestRequestById");
                    return new TestRequestDto();
                }
                _logger.LogInformation("Deserializing response from GetTestRequestById");
                return JsonSerializer.Deserialize<TestRequestDto>(response);
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

        public async Task<GenericResponse> UpdateTestRequest(TestRequestDto testRequestDto)
        {
             _logger.LogInformation("Sending request on topic UpdateTest");            
            try
            {
                var message = JsonSerializer.Serialize(testRequestDto );
                var response = await _msgService.RequestAsync("UpdateTestRequest", message);
                if (string.IsNullOrEmpty(response))
                {
                    _logger.LogWarning("Received empty response from UpdateTestRequest Request");
                    return new GenericResponse(){
                        IsSuccessful= false,
                        Message = "Received empty response from UpdateTestRequest Request"                        
                    };
                }
                _logger.LogInformation("Deserializing response from UpdateTestRequest Request");
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
    }
}