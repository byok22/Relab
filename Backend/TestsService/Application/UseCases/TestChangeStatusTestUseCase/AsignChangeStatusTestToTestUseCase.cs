using Domain.Models.TestModels;
using Domain.Repositories;
using Shared.Response;

namespace Application.UseCases.TestChangeStatusTestUseCases
{
    public class AsignChangeStatusTestToTestUseCase
    {
        
        private readonly ITestChangeStatusTestRepository _repository;


        public AsignChangeStatusTestToTestUseCase(ITestChangeStatusTestRepository repository)
        {
          
            _repository = repository;
           
        }
        
        public async Task<GenericResponse> Execute(int idTest, int idChangeStatusTest)
        {
            var testChangeStatusTest = new TestChangeStatusTest(){
                Id = 0,
                ChangeStatusTestId = idChangeStatusTest,
                TestId = idTest
            };
            var response = await _repository.AssignChangeStatusToTest(testChangeStatusTest);
            return new GenericResponse(){
                IsSuccessful  = response.id>0?true:false,
                Message = response.message
            };

        }
    }
}