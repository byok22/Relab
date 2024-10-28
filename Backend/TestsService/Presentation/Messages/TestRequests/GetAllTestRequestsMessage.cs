using Application.UseCases.TestRequests;
using Domain.Services;
using Presentation.Interfaces;
using Shared.Dtos;

namespace Presentation.Messages.TestRequests
{
    public class GetAllTestRequestsMessage: ISendMessage<GetAllTestRequestUseCase>
    {
        public GetAllTestRequestsMessage(GetAllTestRequestUseCase useCase, IMsgService msgService, ILogger<ISendMessage<GetAllTestRequestUseCase>> logger) : base(useCase, msgService, logger)
        {
        }

        override public async Task Execute()
        {
            try
            {           
                
               await _msgService.SubscribeAsync<List<TestRequestDto>>("GetAllTestRequests",

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