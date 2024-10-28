using Domain.Services;
using Presentation.Interfaces;
using Application.UseCases.CustomersUseCases;
using Shared.Dtos;

namespace Presentation.Messages.Customers
{
    public class GetAllCustomersMessage : ISendMessage<GetAllCustomersUseCase>
    {
        public GetAllCustomersMessage(GetAllCustomersUseCase useCase, IMsgService msgService, ILogger<ISendMessage<GetAllCustomersUseCase>> logger) : base(useCase, msgService, logger)
        {
        }

        override  public async Task Execute()
        {
            try
            {           
                
               await _msgService.SubscribeAsync<List<CustomerDto>>("GetAllCustomers",

               async () =>{
                return await _useCase.Execute();

               });
               
            
              
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing messages");
            }
        }
    }
}