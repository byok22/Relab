
using Application.UseCases.TestAttachmentsUseCases;
using Domain.Services;
using Presentation.Interfaces;
using Presentation.Services.Models;
using Shared.Response;

namespace Presentation.Messages.Test
{
    public class CreateAndAsignAttachmentToTestMessage: ISendMessage<CreateAndAsignAttachmentUseCase>
    {
        public CreateAndAsignAttachmentToTestMessage(CreateAndAsignAttachmentUseCase useCase, IMsgService msgService, ILogger<ISendMessage<CreateAndAsignAttachmentUseCase>> logger) : base(useCase, msgService, logger)
        {
        }
        override public async Task Execute(){

             try
            {           

               await _msgService.SubscribeAsync<AttachmentTest ,GenericResponse>("CreateAttachmentFromTest", async (test) =>{

               
           
                return await _useCase.Execute(test.idtest, test.attachment);

               });
                                         
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing messages");
            }
           
        }    
    }
}