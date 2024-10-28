using Application.UseCases.EmployeeUseCases;
using AutoMapper;
using Domain.Models.Employees;
using Domain.Services;
using Presentation.Interfaces;
using Shared.Dtos;
using Shared.Response;

namespace Presentation.Messages.Employees
{
    public class AddEmployeeMessage : ISendMessage<AddEmployeeUseCase>
    {
        IMapper _mapper;
        public AddEmployeeMessage(AddEmployeeUseCase useCase, IMsgService msgService, ILogger<ISendMessage<AddEmployeeUseCase>> logger, IMapper mapper) : base(useCase, msgService, logger)
        {
            _mapper = mapper;
        }
        override public async Task Execute(){

             try
            {           

               await _msgService.SubscribeAsync<Employee ,GenericResponse>("AddEmployee", async (Employee) =>{

               
                var employeeDto = _mapper.Map<EmployeeDto>(Employee);
                return await _useCase.Execute(employeeDto);

               });
                                         
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing messages");
            }
           
        }    
    }
}