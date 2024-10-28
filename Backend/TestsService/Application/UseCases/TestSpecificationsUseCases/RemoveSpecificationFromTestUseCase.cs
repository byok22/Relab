using Domain.Models.TestModels;
using Domain.Repositories;
using Shared.Response;

namespace Application.UseCases.TestSpecificationsUseCases
{
    public class RemoveSpecificationsFromTestUseCase
    {
        private readonly ITestSpecificationsRepository _repository;
        public RemoveSpecificationsFromTestUseCase(ITestSpecificationsRepository repository)
        {          
            _repository = repository;           
        }        
        public async Task<GenericResponse> Execute(int idTest, int idSpecifications)
        {
            var testSpecifications = new TestSpecification(){
                Id = 0,
                SpecificationId = idSpecifications,
                TestId = idTest
            };
            var response = await _repository.RemoveSpecificationFromTest(testSpecifications);
            return new GenericResponse(){
                IsSuccessful  = response.id>0?true:false,
                Message = response.message
            };

        }
        
    }
}