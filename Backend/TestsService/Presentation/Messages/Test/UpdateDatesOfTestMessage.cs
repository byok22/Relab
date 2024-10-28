using Application.UseCases.Testing;
using Application.UseCases.Tests;
using Domain.Services;
using Presentation.Interfaces;
using Shared.Dtos;
using Shared.Response;

namespace Presentation.Messages.Test
{
    public class UpdateDatesOfTestMessage : ISendMessage<UpdateDatesOfTestUseCase>
    {
        public UpdateDatesOfTestMessage(UpdateDatesOfTestUseCase useCase, IMsgService msgService, ILogger<ISendMessage<UpdateDatesOfTestUseCase>> logger) : base(useCase, msgService, logger)
        {
        }

         override public async Task Execute(){

             try
            {           

               await _msgService.SubscribeAsync<TestDto ,GenericResponse>("UpdateDatesTest",

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