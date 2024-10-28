using Domain.Models.TestModels;
using Domain.Repositories;
using Shared.Response;

namespace Application.UseCases.TestGenericUpdateUseCases
{
    public class AsignGenericUpdateToTestUseCase
    {
        
        private readonly ITestGenericUpdateRepository _repository;


        public AsignGenericUpdateToTestUseCase(ITestGenericUpdateRepository repository)
        {
          
            _repository = repository;
           
        }
        
        public async Task<GenericResponse> Execute(int idTest, int idGenericUpdate)
        {
            var testGenericUpdate = new TestGenericUpdate(){
                Id = 0,
                GenericUpdateId = idGenericUpdate,
                TestId = idTest
            };
            var response = await _repository.AssignGenericUpdateToTest(testGenericUpdate);
            return new GenericResponse(){
                IsSuccessful  = response.id>0?true:false,
                Message = response.message
            };

        }
    }
}