using Domain.Enums;
using Domain.Models;
using Domain.Models.Tests;
using Shared.Dtos;
using Shared.Response;

namespace Domain.Services
{
    public interface ITestsMicroServices
    {
        Task<GenericResponse> AddTest(TestDto testDto);
        Task<GenericResponse> ChangeStatusTest(ChangeStatusTest testDto);
        Task<List<TestDto>> GetAllTest();
        Task<TestDto> GetTestById(int id);
        Task<List<TestDto>> GetAllTestByStatus(TestStatusEnum testStatusEnum);
        Task<GenericResponse> PatchTest(TestDto testDto);
        Task<GenericResponse> DeleteEquipmentFromTest(int idTest, int idEquipment);
        Task<GenericResponse> DeleteSpecificationFromTest(int idTest, int idSpecification);
        Task<GenericResponse> DeleteAttachmentFromTest(int idTest, int idAttachment);
        Task<GenericResponse>  DeleteProfileFromTest(int idTest);
        Task<GenericResponse> DeleteTechnicianFromTest(int idTest, int idEmployee);
        Task<GenericResponse> UpdateDatesTest(int idTest, string startDate, string endDate);
        Task<GenericResponse> CreateAttachment(IFormFile file, string fileName);
         Task<GenericResponse> CreateAttachmentFromTest(int idTest, Attachment attachment);
    }
}