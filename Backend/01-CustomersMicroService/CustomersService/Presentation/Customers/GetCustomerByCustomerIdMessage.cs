using Presentation.Interfaces;
using Domain.Services;
using Shared.Dtos;
using Application.CustomerUseCases;


namespace Presentation.Messages.Customers
{
    public class GetCustomerByCustomerIdMessage : ISendMessage<GetCustomerByCustomerIDUseCase>
    {
        public GetCustomerByCustomerIdMessage(GetCustomerByCustomerIDUseCase useCase, IMsgService msgService, ILogger<ISendMessage<GetCustomerByCustomerIDUseCase>> logger) : base(useCase, msgService, logger)
        {
        }

         override public async Task Execute(){

            try
            {           
                
               await _msgService.SubscribeAsync<string, CustomerDto>("GetCustomerById",

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