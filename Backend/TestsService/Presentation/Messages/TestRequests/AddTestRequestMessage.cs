using Application.UseCases.TestRequests;
using Domain.Models;
using Domain.Services;
using Shared.Dtos;
using Shared.Response;

namespace Presentation.Messages.TestRequests
{
    public class AddTestRequestMessage
    {
        CreateTestsRequestUseCase  _createTestRequestUseCase;
        private readonly IMsgService _msgService;
        private readonly ILogger<GetTestsRequestByStatusMessage> _logger;
        

        public AddTestRequestMessage(CreateTestsRequestUseCase createTestRequestUseCase
        , IMsgService msgService, ILogger<GetTestsRequestByStatusMessage> logger)
        {
            _createTestRequestUseCase = createTestRequestUseCase;      
            _logger = logger;
            _msgService = msgService;
            Execute().ConfigureAwait(false);      
        }

        private async Task Execute()
        {
            try
            {           

               await _msgService.SubscribeAsync<TestRequestDto ,GenericResponse>("AddTestRequest",

               async (status) =>{
               
                return await _createTestRequestUseCase.Execute(status.Description, status.Start, status.End, status.Tests ?? new List<TestDto>(), status.CreatedBy?? new User());

               });
                                         
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing messages");
            }
        }
        
    }
}