using Application.UseCases.Tests;
using Domain.Services;
using Presentation.Interfaces;
using Shared.Dtos;
using Shared.Response;

namespace Presentation.Messages.Test
{
    public class AddTestMessage : ISendMessage<AddTestUseCase>
    {
        public AddTestMessage(AddTestUseCase useCase, IMsgService msgService, ILogger<ISendMessage<AddTestUseCase>> logger) : base(useCase, msgService, logger)
        {
        }
        override public async Task Execute(){

             try
            {           

               await _msgService.SubscribeAsync<TestDto ,GenericResponse>("AddTest", async (test) =>{

               
                return await _useCase.Execute(test);

               });
                                         
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing messages");
            }
           
        }    
    }
}