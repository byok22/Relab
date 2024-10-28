using Domain.Models.TestModels;
using Shared.Dtos;
using Shared.Response;

namespace Domain.Repositories
{
    public interface ITestTechniciansRepository
    {
        public Task<DBResponse> AssignTechnicianToTest(TestTechnicians testTechnician);   
         public Task<DBResponse> RemoveTechnicianFromTest(TestTechnicians testAttachment);
         public Task<List<EmployeeDto>> GetTechniciansFromTest(int idTest);
    }
}