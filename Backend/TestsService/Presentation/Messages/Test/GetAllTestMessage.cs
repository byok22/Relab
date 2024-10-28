using Application.UseCases.Tests;
using Domain.Services;
using Presentation.Interfaces;
using Shared.Dtos;

namespace Presentation.Messages.Test
{
    public class GetAllTestMessage : ISendMessage<GetAllTestsUseCase>
    {
        public GetAllTestMessage(GetAllTestsUseCase useCase, IMsgService msgService, ILogger<ISendMessage<GetAllTestsUseCase>> logger) : base(useCase, msgService, logger)
        {
        }

        override  public async Task Execute()
        {
            try
            {           
                
               await _msgService.SubscribeAsync<List<TestDto>>("GetAllTest",

               async () =>{
                return await _useCase.Execute();

               });
               
            
              
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing messages");
            }
        }
    }
}