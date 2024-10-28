using Domain.Models.TestModels;
using Domain.Repositories;
using Shared.Response;

namespace Application.UseCases.TestTechniciansUseCases
{
    public class RemoveTechnicianFromTestUseCase
    {
        private readonly ITestTechniciansRepository _repository;
        public RemoveTechnicianFromTestUseCase(ITestTechniciansRepository repository)
        {          
            _repository = repository;           
        }        
        public async Task<GenericResponse> Execute(int idTest, int idTechnician)
        {
            var testTechnician = new TestTechnicians(){
                Id = 0,
                EmployeeId = idTechnician,
                TestId = idTest
            };
            var response = await _repository.RemoveTechnicianFromTest(testTechnician);
            return new GenericResponse(){
                IsSuccessful  = response.id>0?true:false,
                Message = response.message
            };

        }
        
    }
}