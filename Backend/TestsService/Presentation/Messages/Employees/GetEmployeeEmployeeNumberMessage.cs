using Presentation.Interfaces;
using Application.UseCases.EmployeeUseCases;
using Domain.Services;
using Shared.Dtos;
using Domain.Models.Employees;
using AutoMapper;


namespace Presentation.Messages.Employees
{
    public class GetEmployeeEmployeeNumberMessage : ISendMessage<GetEmployeeByEmployeeNumberUseCase>
    {
        private readonly IMapper _mapper;
        public GetEmployeeEmployeeNumberMessage(GetEmployeeByEmployeeNumberUseCase useCase, IMsgService msgService, ILogger<ISendMessage<GetEmployeeByEmployeeNumberUseCase>> logger, IMapper mapper) : base(useCase, msgService, logger)
        {
            _mapper = mapper;
        }

         override public async Task Execute(){

            try
            {           
                
               await _msgService.SubscribeAsync<string, Employee>("GetEmployeeByEmployeeNumber",

               async (employeeNumber) =>{
                var employeeDto = await _useCase.Execute(employeeNumber);
                return _mapper.Map<Employee>(employeeDto);

               });
                                         
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing messages");
            }
        }
    }
}