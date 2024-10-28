using Domain.Models.TestModels;
using Domain.Repositories;
using Shared.Response;

namespace Application.UseCases.TestSpecificationsUseCases
{
    public class AsignSpecificationToTestUseCase
    {
        
        private readonly ITestSpecificationsRepository _repository;


        public AsignSpecificationToTestUseCase(ITestSpecificationsRepository repository)
        {
          
            _repository = repository;
           
        }
        
        public async Task<GenericResponse> Execute(int idTest, int idSpecification)
        {
            var testSpecifications = new TestSpecification(){
                Id = 0,
                SpecificationId = idSpecification,
                TestId = idTest
            };
            var response = await _repository.AssignSpecificationToTest(testSpecifications);
            return new GenericResponse(){
                IsSuccessful  = response.id>0?true:false,
                Message = response.message
            };

        }
    }
}