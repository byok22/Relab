using Application.CustomerUseCases;
using Domain.Services;
using Presentation.Interfaces;
using Shared.Dtos;
using Shared.Response;

namespace Presentation.Messages.Customers
{
    public class AddCustomerMessage : ISendMessage<CreateCustomerUseCase>
    {
        public AddCustomerMessage(CreateCustomerUseCase useCase, IMsgService msgService, ILogger<ISendMessage<CreateCustomerUseCase>> logger) : base(useCase, msgService, logger)
        {
        }
        override public async Task Execute(){

             try
            {           

               await _msgService.SubscribeAsync<CustomerDto ,GenericResponse>("AddCustomer", async (Customer) =>{

               
                return await _useCase.Execute(Customer);

               });
                                         
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing messages");
            }
           
        }    
    }
}