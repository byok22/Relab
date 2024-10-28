using AutoMapper;
using Domain.Repositories;
using Shared.Dtos;

namespace Application.UseCases.EmployeeUseCases
{
    public class GetEmployeeByEmployeeNumberUseCase : EmployeeAbstract
    {
        public GetEmployeeByEmployeeNumberUseCase(IEmployeeRepository repository, IMapper mapper) : base(repository, mapper)
        {
        }

          public  async Task<EmployeeDto> Execute(string employeeNumber){         

            var dtos = await _repository.GetByEmployeeNumberAsync(employeeNumber);                
            return _mapper.Map<EmployeeDto>(dtos);
        }
    }
}