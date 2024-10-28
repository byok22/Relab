using Application.UseCases.Testing;
using Domain.Services;
using Presentation.Interfaces;
using Shared.Dtos;
using Shared.Response;

namespace Presentation.Messages.Test
{
    public class PatchTestMessage : ISendMessage<PatchTestUseCase>
    {
        public PatchTestMessage(PatchTestUseCase useCase, IMsgService msgService, ILogger<ISendMessage<PatchTestUseCase>> logger) : base(useCase, msgService, logger)
        {
        }

         override public async Task Execute(){

             try
            {           

               await _msgService.SubscribeAsync<TestDto ,GenericResponse>("PatchTest",

               async (test) =>{
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