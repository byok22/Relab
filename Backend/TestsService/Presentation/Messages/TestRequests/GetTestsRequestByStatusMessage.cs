using Application.UseCases.TestRequests;
using Domain.Services;
using Shared.Request;
using Shared.Dtos;
using Presentation.Interfaces;

namespace Presentation.Messages.TestRequests
{
    
    public class GetTestsRequestByStatusMessage: ISendMessage<GetTestsRequestByStatusUseCase>
    {
        public GetTestsRequestByStatusMessage(GetTestsRequestByStatusUseCase useCase, IMsgService msgService, ILogger<ISendMessage<GetTestsRequestByStatusUseCase>> logger) : base(useCase, msgService, logger)
        {
        }

        override public async Task Execute()
        {
            try
            {           

               await _msgService.SubscribeAsync<TestRequestsRequest ,List<TestRequestDto>>("GetAllTestRequestsByStatus",

               async (status) =>{
                return await _useCase.Execute(status.Status);

               });
               
            
              
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing messages");
            }
        }
        
    }
}