using Presentation.Interfaces;
using Domain.Services;

using Application.UseCases.CustomerUseCases;
using Shared.Dtos;


namespace Presentation.Messages.Customers
{
    public class GetCustomerByIdMessage : ISendMessage<GetCustomerByIdUseCase>
    {
        public GetCustomerByIdMessage(GetCustomerByIdUseCase useCase, IMsgService msgService, ILogger<ISendMessage<GetCustomerByIdUseCase>> logger) : base(useCase, msgService, logger)
        {
        }

         override public async Task Execute(){

            try
            {           
                
               await _msgService.SubscribeAsync<int, CustomerDto>("GetCustomerById",

               async (id) =>{
                return await _useCase.Execute(id);

               });
                                         
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing messages");
            }
        }
    }
}