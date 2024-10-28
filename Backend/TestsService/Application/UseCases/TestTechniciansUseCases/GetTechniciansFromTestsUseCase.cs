using Domain.Repositories;
using Shared.Dtos;

namespace Application.UseCases.TestTechniciansUseCases
{
    public class GetTechniciansFromTestsUseCase
    {
         private readonly ITestTechniciansRepository _repository;


        public GetTechniciansFromTestsUseCase(ITestTechniciansRepository repository)
        {
          
            _repository = repository;
           
        }
        
        public async Task<List<EmployeeDto>> Execute(int idTest)
        {
          
           return await _repository.GetTechniciansFromTest(idTest);           

        }
    }
}