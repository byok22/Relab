using System.Text.Json;
using Domain.Enums;
using Domain.Models;
using Domain.Models.TestModels;
using Domain.Models.Tests;
using Domain.Services;
using Presentation.Services.Models;
using Shared.Dtos;
using Shared.Response;

namespace Presentation.Services
{
    public class TestsMicroServices : ITestsMicroServices
    {
        private readonly IMsgService _msgService;
        private readonly ILogger<TestsMicroServices> _logger;

        public TestsMicroServices(IMsgService msgService, ILogger<TestsMicroServices> logger)
        {
            _msgService = msgService;
            _logger = logger;
        }
        public async Task<GenericResponse> AddTest(TestDto testDto)
        {
            _logger.LogInformation("Sending request on topic AddTest");            
            try
            {
                var message = JsonSerializer.Serialize(testDto);
                var response = await _msgService.RequestAsync("AddTest", message);

                if (string.IsNullOrEmpty(response))
                {
                    _logger.LogWarning("Received empty response from AddTest Request");
                    return new GenericResponse(){
                        IsSuccessful= false,
                        Message = "Received empty response from AddTest Request"                        
                    };
                }
                _logger.LogInformation("Deserializing response from AddTest Request");
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

        public async Task<GenericResponse> ChangeStatusTest(ChangeStatusTest testDto)
        {
            _logger.LogInformation("Sending request on topic ChangeStatusTest");
            try{
                var message = JsonSerializer.Serialize(testDto);
                var response = await _msgService.RequestAsync("ChangeStatusTest", message);

                if (string.IsNullOrEmpty(response))
                {
                    _logger.LogWarning("Received empty response from ChangeStatusTest Request");
                    return new GenericResponse(){
                        IsSuccessful= false,
                        Message = "Received empty response from ChangeStatusTest Request"                        
                    };
                }
                _logger.LogInformation("Deserializing response from ChangeStatusTest Request");
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

        public async Task<GenericResponse> CreateAttachment(IFormFile file, string fileName)
        {
            _logger.LogInformation("Sending request on topic CreateAttachment");
            try
            {
            using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);
            var fileBytes = memoryStream.ToArray();

            var attachment = new Attachment
            {
                Name = fileName,
                File = fileBytes
            };

            var message = JsonSerializer.Serialize(attachment);
            var response = await _msgService.RequestAsync("CreateAttachment", message);

            if (string.IsNullOrEmpty(response))
            {
                _logger.LogWarning("Received empty response from CreateAttachment Request");
                return new GenericResponse{
                    IsSuccessful = false,
                    Message = "Received empty response from CreateAttachment Request"
                    
                };
            }

            _logger.LogInformation("Deserializing response from CreateAttachment Request");
            var result = JsonSerializer.Deserialize<GenericResponse>(response);
            return result;
            }
            catch (JsonException ex)
            {
            _logger.LogError(ex, "Error deserializing the response from the microservice.");
            throw new InvalidOperationException("Error deserializing the response from the microservice.", ex);
            }
            catch (Exception ex)
            {
            _logger.LogError(ex, "Error in the microservice response.");
            throw new InvalidOperationException("Error in the microservice response.", ex);
            }
        }

        public async Task<GenericResponse> CreateAttachmentFromTest(int idTest, Attachment attachment)
        {
            AttachmentTest attachmentTest = new AttachmentTest();
            _logger.LogInformation("Sending request on topic CreateAttachmentFromTest");
            try
            {
            attachmentTest.idtest = idTest;
            attachmentTest.attachment = attachment;
            var message = JsonSerializer.Serialize(attachmentTest);
            var response = await _msgService.RequestAsync("CreateAttachmentFromTest", message);

            if (string.IsNullOrEmpty(response))
            {
                _logger.LogWarning("Received empty response from CreateAttachmentFromTest Request");
                return new GenericResponse()
                {
                IsSuccessful = false,
                Message = "Received empty response from CreateAttachmentFromTest Request"
                };
            }
            _logger.LogInformation("Deserializing response from CreateAttachmentFromTest Request");
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

        public async Task<GenericResponse> DeleteAttachmentFromTest(int idTest, int idAttachment)
        {
            _logger.LogInformation("Sending request on topic DeleteAttachmentFromTest");
            try
            {
                TestAttachment testAttachment = new TestAttachment();
                testAttachment.TestId = idTest;
                testAttachment.AttachmentId = idAttachment;

            var message = JsonSerializer.Serialize(testAttachment);
            var response = await _msgService.RequestAsync("DeleteAttachmentFromTest", message);

            if (string.IsNullOrEmpty(response))
            {
                _logger.LogWarning("Received empty response from DeleteAttachmentFromTest Request");
                return new GenericResponse()
                {
                IsSuccessful = false,
                Message = "Received empty response from DeleteAttachmentFromTest Request"
                };
            }
            _logger.LogInformation("Deserializing response from DeleteAttachmentFromTest Request");
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

        public async Task<GenericResponse> DeleteEquipmentFromTest(int idTest, int idEquipment)
        {
            _logger.LogInformation("Sending request on topic DeleteEquipmentFromTest");
            try
            {
             TestEquipment testEquipment = new TestEquipment();
                testEquipment.TestId = idTest;
                testEquipment.EquipmentId = idEquipment;

            var message = JsonSerializer.Serialize(testEquipment);
            var response = await _msgService.RequestAsync("DeleteEquipmentFromTest", message);

            if (string.IsNullOrEmpty(response))
            {
                _logger.LogWarning("Received empty response from DeleteEquipmentFromTest Request");
                return new GenericResponse()
                {
                IsSuccessful = false,
                Message = "Received empty response from DeleteEquipmentFromTest Request"
                };
            }
            _logger.LogInformation("Deserializing response from DeleteEquipmentFromTest Request");
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

        public async Task<GenericResponse> DeleteProfileFromTest(int idTest)
        {
            _logger.LogInformation("Sending request on topic DeleteProfileFromTest");
            try
            {
            var message = JsonSerializer.Serialize(idTest);
            var response = await _msgService.RequestAsync("DeleteProfileFromTest", message);

            if (string.IsNullOrEmpty(response))
            {
                _logger.LogWarning("Received empty response from DeleteProfileFromTest Request");
                return new GenericResponse()
                {
                IsSuccessful = false,
                Message = "Received empty response from DeleteProfileFromTest Request"
                };
            }
            _logger.LogInformation("Deserializing response from DeleteProfileFromTest Request");
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

        public async Task<GenericResponse> DeleteSpecificationFromTest(int idTest, int idSpecification)
        {
            _logger.LogInformation("Sending request on topic DeleteSpecificationFromTest");
            try
            {
            TestSpecification testSpecification = new TestSpecification();
                testSpecification.TestId = idTest;
                testSpecification.SpecificationId = idSpecification;

            var message = JsonSerializer.Serialize(testSpecification);
            var response = await _msgService.RequestAsync("DeleteSpecificationFromTest", message);

            if (string.IsNullOrEmpty(response))
            {
                _logger.LogWarning("Received empty response from DeleteSpecificationFromTest Request");
                return new GenericResponse()
                {
                IsSuccessful = false,
                Message = "Received empty response from DeleteSpecificationFromTest Request"
                };
            }
            _logger.LogInformation("Deserializing response from DeleteSpecificationFromTest Request");
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

        public async Task<GenericResponse> DeleteTechnicianFromTest(int idTest, int idEmployee)
        {
            _logger.LogInformation("Sending request on topic DeleteTechnicianFromTest");
            try
            {
             TestTechnicians testTechnicians = new TestTechnicians();
                testTechnicians.TestId = idTest;
                testTechnicians.EmployeeId = idEmployee;

            var message = JsonSerializer.Serialize(testTechnicians);
            var response = await _msgService.RequestAsync("DeleteTechnicianFromTest", message);

            if (string.IsNullOrEmpty(response))
            {
                _logger.LogWarning("Received empty response from DeleteTechnicianFromTest Request");
                return new GenericResponse()
                {
                IsSuccessful = false,
                Message = "Received empty response from DeleteTechnicianFromTest Request"
                };
            }
            _logger.LogInformation("Deserializing response from DeleteTechnicianFromTest Request");
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

        public async Task<List<TestDto>> GetAllTest()
        {
             _logger.LogInformation("Sending request on topic GetAllTest");            
            try
            {
                var response = await _msgService.RequestAsync("GetAllTest");

                if (string.IsNullOrEmpty(response))
                {
                    _logger.LogWarning("Received empty response from GetAllTest");
                    return new List<TestDto>();
                }
                _logger.LogInformation("Deserializing response from GetAllTest");
                return JsonSerializer.Deserialize<List<TestDto>>(response);
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

        public async Task<List<TestDto>> GetAllTestByStatus(TestStatusEnum testStatusEnum)
        {
             _logger.LogInformation("Sending request on topic GetAllTestByStatus");            
            try
            {
                var message = JsonSerializer.Serialize(testStatusEnum);
                var response = await _msgService.RequestAsync("GetAllTestByStatus", message);

                if (string.IsNullOrEmpty(response))
                {
                    _logger.LogWarning("Received empty response from GetAllTestByStatus");
                    return new List<TestDto>();
                }
                _logger.LogInformation("Deserializing response from GetAllTestByStatus");
                return JsonSerializer.Deserialize<List<TestDto>>(response);
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

        public async Task<TestDto> GetTestById(int id)
        {
              _logger.LogInformation("Sending request on topic GetTestById");            
            try
            {
                var message = JsonSerializer.Serialize(id);
                var response = await _msgService.RequestAsync("GetTestById", message);

                if (string.IsNullOrEmpty(response))
                {
                    _logger.LogWarning("Received empty response from GetTestById");
                    return new TestDto();
                }
                _logger.LogInformation("Deserializing response from GetTestById");
                return JsonSerializer.Deserialize<TestDto>(response);
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

        public async Task<GenericResponse> PatchTest(TestDto testDto)
        {
             _logger.LogInformation("Sending request on topic AddTest");            
            try
            {
                var message = JsonSerializer.Serialize(testDto );
                var response = await _msgService.RequestAsync("PatchTest", message);

                if (string.IsNullOrEmpty(response))
                {
                    _logger.LogWarning("Received empty response from PatchTest Request");
                    return new GenericResponse(){
                        IsSuccessful = false,
                        Message = "Received empty response from PatchTest Request"
                        
                    };
                }
                _logger.LogInformation("Deserializing response from PatchTest Request");
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

        public async  Task<GenericResponse> UpdateDatesTest(int idTest, string startDate, string endDate)
        {
                //convert string to datetime
                var startDateDate = DateTime.Parse(startDate);
                var endDateDate = DateTime.Parse(endDate);

                var testDates = new TestDto();
                testDates.Id = idTest;
                testDates.Start = startDateDate;
                testDates.End = endDateDate;

                _logger.LogInformation("Sending request on topic UpdateDatesTest");
                try
                {
                    var message = JsonSerializer.Serialize(testDates);
                    var response = await _msgService.RequestAsync("UpdateDatesTest", message);

                    if (string.IsNullOrEmpty(response))
                    {
                        _logger.LogWarning("Received empty response from UpdateDatesTest Request");
                        return new GenericResponse()
                        {
                            IsSuccessful = false,
                            Message = "Received empty response from UpdateDatesTest Request"
                        };
                        
                    }   
                    _logger.LogInformation("Deserializing response from UpdateDatesTest Request");
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