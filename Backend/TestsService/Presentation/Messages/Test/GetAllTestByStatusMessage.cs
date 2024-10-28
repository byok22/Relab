using Application.UseCases.Tests;
using Domain.Enums;
using Domain.Services;
using Presentation.Interfaces;
using Shared.Dtos;

namespace Presentation.Messages.Test
{
    public class GetAllTestByStatusMessage : ISendMessage<GetAllTestsByStatusUseCase>
    {
        public GetAllTestByStatusMessage(GetAllTestsByStatusUseCase useCase, IMsgService msgService, ILogger<ISendMessage<GetAllTestsByStatusUseCase>> logger) : base(useCase, msgService, logger)
        {
        }

        override public async Task Execute(){

            try
            {           
                
               await _msgService.SubscribeAsync<TestStatusEnum, List<TestDto>>("GetAllTestByStatus",

               async (status) =>{
                return await _useCase.Execute(status);

               });
                                         
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing messages");
            }
        }
    }
}