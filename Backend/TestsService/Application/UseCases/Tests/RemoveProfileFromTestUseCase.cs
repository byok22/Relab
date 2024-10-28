using Domain.Repositories;
using Shared.Response;

namespace Application.UseCases.Tests
{
    public class RemoveProfileFromTestUseCase
    {
                private readonly ITestRepository _repository;
        public RemoveProfileFromTestUseCase(ITestRepository repository)
        {          
            _repository = repository;           
        }        
        public async Task<GenericResponse> Execute(int idTest)
        {
           
            var response = await _repository.RemoveProfileFromTest(idTest);
            return new GenericResponse(){
                IsSuccessful  = response.id>0?true:false,
                Message = response.message
            };

        }
        
    }
}