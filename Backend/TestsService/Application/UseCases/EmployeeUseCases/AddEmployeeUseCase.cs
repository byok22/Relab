using AutoMapper;
using Shared.Response;
using Domain.Repositories;
using Shared.Dtos;
using Domain.Models.Employees;
namespace Application.UseCases.EmployeeUseCases
{
    public class AddEmployeeUseCase : EmployeeAbstract
    {
        public AddEmployeeUseCase(IEmployeeRepository repository, IMapper mapper) : base(repository, mapper)
        {
        }

        public  async Task<GenericResponse> Execute(EmployeeDto EmployeeDto){

            var Employeev = _mapper.Map<Employee>(EmployeeDto);

            if(Employeev != null){

                var att = await _repository.AddAsync(Employeev);
                return new GenericResponse(){
                    IsSuccessful = true,
                    Message = $"Employee Saved saved.",
                    Id = att.Id,
                };
            }
            return new GenericResponse(){
                IsSuccessful = false,
                Message = $"Not Saved"
            };

        }
    }
}