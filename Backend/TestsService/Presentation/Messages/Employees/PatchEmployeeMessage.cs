using Application.UseCases.EmployeeUseCases;
using AutoMapper;
using Domain.Models.Employees;
using Domain.Services;
using Presentation.Interfaces;
using Shared.Dtos;
using Shared.Response;

namespace Presentation.Messages.Employees
{
    public class PatchEmployeeMessage : ISendMessage<UpdateEmployeeUseCase>
    {
        IMapper _mapper;
        public PatchEmployeeMessage(UpdateEmployeeUseCase useCase, IMsgService msgService, ILogger<ISendMessage<UpdateEmployeeUseCase>> logger, IMapper mapper) : base(useCase, msgService, logger)
        {
            _mapper = mapper;
        }

         override public async Task Execute(){

             try
            {           

               await _msgService.SubscribeAsync<Employee ,GenericResponse>("PatchEmployee",

               async (Employee) =>{
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