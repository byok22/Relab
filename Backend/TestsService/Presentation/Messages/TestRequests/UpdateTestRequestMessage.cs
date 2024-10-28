using Application.UseCases.TestRequests;
using Domain.Models;
using Domain.Services;
using Presentation.Interfaces;
using Shared.Dtos;
using Shared.Response;

namespace Presentation.Messages.TestRequests
{
    public class UpdateTestRequestMessage : ISendMessage<UpdateTestRequestUseCase>
    {
        public UpdateTestRequestMessage(UpdateTestRequestUseCase useCase, IMsgService msgService, ILogger<ISendMessage<UpdateTestRequestUseCase>> logger) : base(useCase, msgService, logger)
        {
        }

        override public async Task Execute()
        {
             try
            {           

               await _msgService.SubscribeAsync<TestRequestDto ,GenericResponse>("UpdateTestRequest",

               async (status) =>{
               
                return await _useCase.Execute(status.Id,status.Description, status.Start, status.End, status.Tests ?? new List<TestDto>(), status.CreatedBy?? new User());

               });
                                         
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing messages");
            }
        }


    }
}