using Domain.Services;
using Presentation.Interfaces;
using Application.UseCases.EmployeeUseCases;
using Shared.Dtos;
using AutoMapper;
using Domain.Models.Employees;

namespace Presentation.Messages.Employees
{
    public class GetAllEmployeesMessage : ISendMessage<GetAllEmployeesUseCase>
    {
        IMapper _mapper;
        public GetAllEmployeesMessage(GetAllEmployeesUseCase useCase, IMsgService msgService, ILogger<ISendMessage<GetAllEmployeesUseCase>> logger
        , IMapper mapper
        )
         : base(useCase, msgService, logger)
        {
            _mapper = mapper;
        }

        override  public async Task Execute()
        {
            try
            {           
                
               await _msgService.SubscribeAsync<List<Employee>>("GetAllEmployees",

               async () =>{
                var results =  await _useCase.Execute();
                var employeesEntities = _mapper.Map<List<Employee>>(results);

                return employeesEntities;

               });
               
            
              
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing messages");
            }
        }
    }
}