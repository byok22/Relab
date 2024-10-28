using AutoMapper;
using Shared.Dtos;
using Domain.Repositories;

namespace Application.UseCases.EmployeeUseCases
{
    public class GetEmployeeByIdUseCase : EmployeeAbstract
    {
        public GetEmployeeByIdUseCase(IEmployeeRepository repository, IMapper mapper) : base(repository, mapper)
        {
        }

          public  async Task<EmployeeDto> Execute(int id){         

            var dtos = await _repository.GetByIdAsync(id);                
            return _mapper.Map<EmployeeDto>(dtos);
        }
    }
}