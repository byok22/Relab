
using System.Text.Json;
using Application.UseCases.TestSpecificationsUseCases;
using Domain.Models.TestModels;
using Domain.Services;
using Presentation.Interfaces;
using Shared.Response;

namespace Presentation.Messages.Test
{
    public class DeleteSpecificationFromTestMessage : ISendMessage<RemoveSpecificationsFromTestUseCase>
    {
        public DeleteSpecificationFromTestMessage(RemoveSpecificationsFromTestUseCase useCase, IMsgService msgService, ILogger<ISendMessage<RemoveSpecificationsFromTestUseCase>> logger) : base(useCase, msgService, logger)
        {
        }
         override public async Task Execute(){

            


            try
            {    
                //Subscribe to the message queue and execute the use case received idTest and idSpecification as parameters response is a GenericResponse   Task SubscribeAsync<TRequest, TResponse>(string subject, Func<TRequest, Task<TResponse>> messageHandler);

               
                
               await _msgService.SubscribeAsync<TestSpecification, GenericResponse>("DeleteSpecificationFromTest",

               async (message) =>{
                return await _useCase.Execute(message.TestId, message.SpecificationId);

               });
                                         
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing messages");
            }
        }
    }
}