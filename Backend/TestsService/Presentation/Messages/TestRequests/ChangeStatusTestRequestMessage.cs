using Application.UseCases.TestRequests;
using Domain.Models.TestRequests;
using Domain.Services;
using Presentation.Interfaces;
using Shared.Request;
using Shared.Response;

namespace Presentation.Messages.TestRequests
{
    public class ChangeStatusTestRequestMessage : ISendMessage<ChangeStatusTestRequestUseCase>
    {
        public ChangeStatusTestRequestMessage(ChangeStatusTestRequestUseCase useCase, IMsgService msgService, ILogger<ISendMessage<ChangeStatusTestRequestUseCase>> logger) : base(useCase, msgService, logger)
        {
        }
        override public async Task Execute(){

            try
            {                           
               await _msgService.SubscribeAsync<ChangeStatusTestRequestApi, GenericResponse>("ChangeStatusTestRequest",

               async (message) =>{
                return await _useCase.Execute(message.id, message.changeStatusTestRequest?? new ChangeStatusTestRequest());

               });
                                         
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing messages");
            }
        }
    }
}