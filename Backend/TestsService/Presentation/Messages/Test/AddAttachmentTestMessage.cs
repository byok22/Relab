using Application.UseCases.AttachmentUseCases;
using Domain.Models;
using Domain.Services;
using Presentation.Interfaces;
using Shared.Dtos;
using Shared.Response;

namespace Presentation.Messages.Test
{
    public class AddAttachmentTestMessage : ISendMessage<AddAttachmentUseCase>
    {
        public AddAttachmentTestMessage(AddAttachmentUseCase useCase, IMsgService msgService, ILogger<ISendMessage<AddAttachmentUseCase>> logger) : base(useCase, msgService, logger)
        {
        }
        override public async Task Execute(){

             try
            {           

               await _msgService.SubscribeAsync<Attachment ,GenericResponse>("CreateAttachment", async (test) =>{

               
                var attachmentDto = new AttachmentDto
                {
                    // Map properties from test (Attachment) to attachmentDto (AttachmentDto)
                    // Example:
                     Id = test.Id,
                     Name = test.Name,
                     File = test.File?? new byte[0],

                  
                };
                return await _useCase.Execute(attachmentDto);

               });
                                         
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing messages");
            }
           
        }    
    }
}