using Domain.Models;
using Domain.Models.TestModels;
using Shared.Response;

namespace Domain.Repositories
{
    public interface ITestSpecificationsRepository
    {
        public Task<DBResponse> AssignSpecificationToTest(TestSpecification testSpecification);   
         public Task<DBResponse> RemoveSpecificationFromTest(TestSpecification testAttachment);
        public Task<List<Specification>> GetSpecificationsByTestId(int testId);
    }
}