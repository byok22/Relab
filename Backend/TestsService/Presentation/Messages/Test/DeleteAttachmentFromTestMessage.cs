
using System.Text.Json;
using Application.UseCases.TestAttachmentsUseCases;
using Domain.Models.TestModels;
using Domain.Services;
using Presentation.Interfaces;
using Shared.Response;

namespace Presentation.Messages.Test
{
    public class DeleteAttachmentFromTestMessage : ISendMessage<RemoveAttachmentFromTestUseCase>
    {
        public DeleteAttachmentFromTestMessage(RemoveAttachmentFromTestUseCase useCase, IMsgService msgService, ILogger<ISendMessage<RemoveAttachmentFromTestUseCase>> logger) : base(useCase, msgService, logger)
        {
        }
         override public async Task Execute(){

            


            try
            {    
                //Subscribe to the message queue and execute the use case received idTest and idAttachment as parameters response is a GenericResponse   Task SubscribeAsync<TRequest, TResponse>(string subject, Func<TRequest, Task<TResponse>> messageHandler);

               
                
               await _msgService.SubscribeAsync<TestAttachment, GenericResponse>("DeleteAttachmentFromTest",

               async (message) =>{
                return await _useCase.Execute(message.TestId, message.AttachmentId);

               });
                                         
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing messages");
            }
        }
    }
}