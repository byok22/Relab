using Domain.Models.TestModels;
using Domain.Repositories;
using Shared.Response;

namespace Application.UseCases.TestTechniciansUseCases
{
    public class AsignTechnicianToTestUseCase
    {
        
        private readonly ITestTechniciansRepository _repository;


        public AsignTechnicianToTestUseCase(ITestTechniciansRepository repository)
        {
          
            _repository = repository;
           
        }
        
        public async Task<GenericResponse> Execute(int idTest, int idTechnician)
        {
            var testTechnicians = new TestTechnicians(){
                Id = 0,
                EmployeeId = idTechnician,
                TestId = idTest
            };
            var response = await _repository.AssignTechnicianToTest(testTechnicians);
            return new GenericResponse(){
                IsSuccessful  = response.id>0?true:false,
                Message = response.message
            };

        }
    }
}