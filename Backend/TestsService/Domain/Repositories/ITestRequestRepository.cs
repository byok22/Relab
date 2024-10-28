

using Domain.Enums;
using Domain.Models;
using Domain.Models.TestRequests;
using Shared.Dtos;

namespace Domain.Repositories
{
    public interface ITestRequestRepository: IGenericRepository<TestRequest>
    {
         Task<List<TestRequest>> GetTestRequestsByStatus(TestRequestsStatus? status);
        Task<TestRequest> AproveOrDennyTestRequest(int id, ChangeStatusTestRequest changeStatusTestRequest);
        
        
        Task<TestRequest> ChangeStatusTestRequest(int id, ChangeStatusTestRequest changeStatusTestRequest);
        Task<List<Test>> GetTestOfTestRequest(int idTestRequest);
    }
}