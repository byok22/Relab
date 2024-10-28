using AutoMapper;
using Domain.Repositories;
using Shared.Dtos;
using Shared.Response;
using Domain.Models.Generics;
using Domain.Models.Employees;

namespace Application.UseCases.EmployeeUseCases
{
    public class DeleteEmployeeUseCase : EmployeeAbstract
    {
        public DeleteEmployeeUseCase(IEmployeeRepository repository, IMapper mapper) : base(repository, mapper)
        {
        }

        public async Task<GenericResponse> Execute(EmployeeDto EmployeeDto)
        {
            var EmployeeV = _mapper.Map<Employee>(EmployeeDto);
            if (EmployeeV == null)
                throw new ArgumentNullException(nameof(Employee));


           var result = await _repository.RemoveAsync(EmployeeV);
            return new GenericResponse
            {
                IsSuccessful = result.id>0?true:false,
                Message = result.message
            };
            
        }
    }
    
}