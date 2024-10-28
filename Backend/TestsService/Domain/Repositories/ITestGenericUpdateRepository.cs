
using Domain.Models.Generics;
using Domain.Models.TestModels;

using Shared.Response;

namespace Domain.Repositories
{
    public interface ITestGenericUpdateRepository
    {
         public Task<DBResponse> AssignGenericUpdateToTest(TestGenericUpdate genericUpdate);   
         public Task<DBResponse> RemoveGenericUpdateFromTest(TestGenericUpdate genericUpdate);
        public Task<List<GenericUpdate>> GetGenericUpdatesByTestId(int testId);
        
    }
}