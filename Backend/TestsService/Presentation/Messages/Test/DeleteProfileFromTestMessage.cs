
using Application.UseCases.Tests;
using Domain.Services;
using Presentation.Interfaces;
using Shared.Response;

namespace Presentation.Messages.Test
{
    public class DeleteProfileFromTestMessage : ISendMessage<RemoveProfileFromTestUseCase>
    {
        public DeleteProfileFromTestMessage(RemoveProfileFromTestUseCase useCase, IMsgService msgService, ILogger<ISendMessage<RemoveProfileFromTestUseCase>> logger) : base(useCase, msgService, logger)
        {
        }
         override public async Task Execute(){

            


            try
            {    
                //Subscribe to the message queue and execute the use case received idTest and idProfile as parameters response is a GenericResponse   Task SubscribeAsync<TRequest, TResponse>(string subject, Func<TRequest, Task<TResponse>> messageHandler);

               
                
               await _msgService.SubscribeAsync<int, GenericResponse>("DeleteProfileFromTest",

               async (message) =>{
                return await _useCase.Execute(message);

               });
                                         
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing messages");
            }
        }
    }
}