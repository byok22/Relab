using Application.UseCases.ChangeStatusTestUseCases;
using Domain.Models;
using Domain.Services;
using Presentation.Interfaces;
using Shared.Response;

namespace Presentation.Messages.Tests
{
    public class ChangeStatusTestMessage : ISendMessage<AddChangeStatusTestUseCase>
    {
        public ChangeStatusTestMessage(AddChangeStatusTestUseCase useCase, IMsgService msgService, ILogger<ISendMessage<AddChangeStatusTestUseCase>> logger) : base(useCase, msgService, logger)
        {
        }
        override public async Task Execute(){

            try
            {                           
               await _msgService.SubscribeAsync<ChangeStatusTest, GenericResponse>("ChangeStatusTest",

               async (message) =>{
                return await _useCase.Execute(message?? new ChangeStatusTest());

               });
                                         
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing messages");
            }
        }
    }
}