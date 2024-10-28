using Presentation.Interfaces;
using Application.UseCases.Tests;
using Domain.Services;
using Shared.Dtos;

namespace Presentation.Messages.Test
{
    public class GetTestByIdMessage : ISendMessage<GetTestByIdUseCase>
    {
        public GetTestByIdMessage(GetTestByIdUseCase useCase, IMsgService msgService, ILogger<ISendMessage<GetTestByIdUseCase>> logger) : base(useCase, msgService, logger)
        {
        }

         override public async Task Execute(){

            try
            {           
                
               await _msgService.SubscribeAsync<int, TestDto>("GetTestById",

               async (id) =>{
                return await _useCase.Execute(id);

               });
                                         
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing messages");
            }
        }
    }
}