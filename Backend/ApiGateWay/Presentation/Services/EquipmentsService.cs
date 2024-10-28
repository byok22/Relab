using System.Text.Json;
using Domain.Enums;
using Domain.Models.Equipments;
using Domain.Models.Generics;
using Domain.Services;
using Shared.Dtos;
using Shared.Request;
using Shared.Response;

namespace Presentation.Services
{
    public class EquipmentsService : IEquipmentsMicroServices
    {
        private readonly IMsgService _msgService;
        private readonly ILogger<EquipmentsService> _logger;

        public EquipmentsService(IMsgService msgService, ILogger<EquipmentsService> logger)
        {
            _msgService = msgService;
            _logger = logger;
        }

        public async Task<GenericResponse> AddEquipment(Equipment equipment)
        {
            _logger.LogInformation("Sending request on topic AddEquipment");            
            try
            {
                var message = JsonSerializer.Serialize( equipment );
                var response = await _msgService.RequestAsync("AddEquipment", message);
                if (string.IsNullOrEmpty(response))
                {
                    _logger.LogWarning("Received empty response from AddEquipment Request");
                    return new GenericResponse(){
                        IsSuccessful= false,
                        Message = "Received empty response from AddEquipment Request"                        
                    };
                }
                _logger.LogInformation("Deserializing response from AddEquipment Request");
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

       
        public async Task<List<Equipment>> GetAllEquipments()
        {
             _logger.LogInformation("Sending request on topic GetAllEquipments");            
            try
            {
                var response = await _msgService.RequestAsync("GetAllEquipments");

                if (string.IsNullOrEmpty(response))
                {
                    _logger.LogWarning("Received empty response from GetAllEquipments");
                    return new List<Equipment>();
                }
                _logger.LogInformation("Deserializing response from GetAllEquipments");
                return JsonSerializer.Deserialize<List<Equipment>>(response);
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

             public async Task<Equipment> GetEquipmentById(int id)
        {
             _logger.LogInformation("Sending request on topic GetEquipmentById");            
            try
            {
                var message = JsonSerializer.Serialize(id);
                var response = await _msgService.RequestAsync("GetEquipmentById", message);

                if (string.IsNullOrEmpty(response))
                {
                    _logger.LogWarning("Received empty response from GetEquipmentById");
                    return new Equipment();
                }
                _logger.LogInformation("Deserializing response from GetEquipmentById");
                return JsonSerializer.Deserialize<Equipment>(response);
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

        public Task<List<DropDown>> GetEquipmentsStatus()
        {
            //GetEquipmentStatus
            _logger.LogInformation("Sending request on topic GetEquipmentStatus");
            try
            {
                var response = _msgService.RequestAsync("GetEquipmentStatus").Result;
                if (string.IsNullOrEmpty(response))
                {
                    _logger.LogWarning("Received empty response from GetEquipmentStatus Request");
                    return Task.FromResult(new List<DropDown>());
                }
                _logger.LogInformation("Deserializing response from GetEquipmentStatus Request");
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

        public async Task<GenericResponse> PatchEquipment(Equipment equipment)
        {
             _logger.LogInformation("Sending request on topic PatchTest");            
            try
            {
                var message = JsonSerializer.Serialize( equipment );
                var response = await _msgService.RequestAsync("PatchEquipment", message);
                if (string.IsNullOrEmpty(response))
                {
                    _logger.LogWarning("Received empty response from PatchEquipment Request");
                    return new GenericResponse(){
                        IsSuccessful= false,
                        Message = "Received empty response from PatchEquipment Request"                        
                    };
                }
                _logger.LogInformation("Deserializing response from PatchEquipment Request");
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

        public async Task<GenericResponse> RemoveEquipment(int id)
        {
             _logger.LogInformation("Sending request on topic RemoveEquipment");            
            try
            {
                var message = JsonSerializer.Serialize(id);
                var response = await _msgService.RequestAsync("RemoveEquipment", message);
                if (string.IsNullOrEmpty(response))
                {
                    _logger.LogWarning("Received empty response from RemoveEquipment Request");
                    return new GenericResponse(){
                        IsSuccessful= false,
                        Message = "Received empty response from RemoveEquipment Request"                        
                    };
                }
                _logger.LogInformation("Deserializing response from RemoveEquipment Request");
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