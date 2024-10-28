using Domain.Services;
using Presentation.Interfaces;
using Application.UseCases.EmployeeUseCases;
using Shared.Dtos;
using AutoMapper;
using Domain.Models.Employees;

namespace Presentation.Messages.Employees
{
    public class GetEmployeesByTypeMessage : ISendMessage<GetEmployeesByTypeUseCase>
    {
        IMapper _mapper;
        public GetEmployeesByTypeMessage(GetEmployeesByTypeUseCase useCase, IMsgService msgService, ILogger<ISendMessage<GetEmployeesByTypeUseCase>> logger
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
                
               await _msgService.SubscribeAsync<EmployeeTypeEnum,List<Employee>>("GetEmployeesByType",

               async (type) =>{
                var results =  await _useCase.Execute(type);
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