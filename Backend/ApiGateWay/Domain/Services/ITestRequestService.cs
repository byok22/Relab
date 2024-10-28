using Domain.Enums;
using Domain.Models.TestRequests;
using Shared.Dtos;
using Shared.Response;

namespace Domain.Services
{
    public interface ITestRequestService
    {
         //Test Request
        Task<GenericResponse> AddTestRequest(TestRequestDto testRequestDto);
        Task<GenericResponse> UpdateTestRequest(TestRequestDto testRequestDto);
        Task<List<TestRequestDto>> GetAllTestRequests();
        Task<List<TestRequestDto>> GetAllTestRequestsByStatus(TestRequestsStatus testRequestsStatus);

        Task<GenericResponse> ApproveOrRejectTestRequest(int id,  ChangeStatusTestRequest changeStatusTestRequest);  
        Task<GenericResponse> ChangeStatusTestRequest(int id,  ChangeStatusTestRequest changeStatusTestRequest);  

        Task<TestRequestDto> GetTestRequestsById(int id);
    }
}