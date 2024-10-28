using Application.UseCases.CustomersUseCases;

using Domain.Services;
using Presentation.Interfaces;
using Shared.Dtos;
using Shared.Response;

namespace Presentation.Messages.Customers
{
    public class PatchCustomerMessage : ISendMessage<UpdateCustomerUseCase>
    {
        public PatchCustomerMessage(UpdateCustomerUseCase useCase, IMsgService msgService, ILogger<ISendMessage<UpdateCustomerUseCase>> logger) : base(useCase, msgService, logger)
        {
        }

         override public async Task Execute(){

             try
            {           

               await _msgService.SubscribeAsync<CustomerDto ,GenericResponse>("PatchCustomer",

               async (Customer) =>{
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