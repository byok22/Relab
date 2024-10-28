using AutoMapper;
using Shared.Dtos;
using Domain.Repositories;
using Domain.Models.Employees;

namespace Application.UseCases.EmployeeUseCases
{
    public class GetEmployeesByTypeUseCase : EmployeeAbstract
    {
        public GetEmployeesByTypeUseCase(IEmployeeRepository repository, IMapper mapper) : base(repository, mapper)
        {
        }

          public  async Task<List<EmployeeDto>> Execute(EmployeeTypeEnum employeeTypeEnum){         

            var dtos = await _repository.GetByEmployeebyType(employeeTypeEnum);                
            return _mapper.Map<List<EmployeeDto>>(dtos);
        }
    }
}