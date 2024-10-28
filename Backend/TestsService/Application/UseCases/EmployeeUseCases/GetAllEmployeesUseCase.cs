using AutoMapper;
using Shared.Dtos;
using Domain.Repositories;

namespace Application.UseCases.EmployeeUseCases
{
    public class GetAllEmployeesUseCase : EmployeeAbstract
    {
        public GetAllEmployeesUseCase(IEmployeeRepository repository, IMapper mapper) : base(repository, mapper)
        {
        }

          public  async Task<List<EmployeeDto>> Execute(){         

            var dtos = await _repository.GetAllAsync();                
            return _mapper.Map<List<EmployeeDto>>(dtos);
        }
    }
}