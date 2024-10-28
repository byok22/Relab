using Application.UseCases.TestRequests;
using Domain.Services;
using Presentation.Interfaces;
using Shared.Dtos;

namespace Presentation.Messages.TestRequests
{
    public class GetTestRequestByIdMessage : ISendMessage<GetTestRequestByIdUseCase>
    {
        public GetTestRequestByIdMessage(GetTestRequestByIdUseCase useCase, IMsgService msgService, ILogger<ISendMessage<GetTestRequestByIdUseCase>> logger) : base(useCase, msgService, logger)
        {
        }

        override public async Task Execute(){

            try
            {           
                
               await _msgService.SubscribeAsync<int, TestRequestDto>("GetTestRequestById",

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