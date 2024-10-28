using Domain.Models;
using Domain.Models.TestModels;
using Shared.Response;

namespace Domain.Repositories
{
    public interface ITestChangeStatusTestRepository
    {
         public Task<DBResponse> AssignChangeStatusToTest(TestChangeStatusTest changeStatus);   
         public Task<DBResponse> RemoveChangeStatusFromTest(TestChangeStatusTest changeStatus);
         public Task<List<ChangeStatusTest>> GetStatusByTestId(int testId);
        
    }
}