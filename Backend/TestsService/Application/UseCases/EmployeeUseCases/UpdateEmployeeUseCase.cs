using AutoMapper;
using Domain.Models.Generics;
using Shared.Dtos;
using Shared.Response;
using Domain.Repositories;
using Domain.Models.Employees;

namespace Application.UseCases.EmployeeUseCases
{
    public class UpdateEmployeeUseCase : EmployeeAbstract
    {
        public UpdateEmployeeUseCase(IEmployeeRepository repository, IMapper mapper) : base(repository, mapper)
        {
        }
         public  async Task<GenericResponse> Execute(EmployeeDto EmployeeDto)
        {     
          
            


            var objs = _mapper.Map<Employee>(EmployeeDto);


            try{
                await _repository.UpdateAsync(objs);

                    return new GenericResponse{
                    IsSuccessful =true,
                    Message = "Update Employee"
                };

            }catch(Exception ex){

                 return  new GenericResponse{
                 IsSuccessful =false,
                 Message = "Error Update Employee "+ex.Message,
                };

            }
            


           

            

            


            //enviarlo al Repository


            //Send Notification


        
        }
    }
}