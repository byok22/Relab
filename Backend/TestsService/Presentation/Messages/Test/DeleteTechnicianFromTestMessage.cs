using Application.UseCases.TestTechniciansUseCases;
using Domain.Models.TestModels;
using Domain.Services;
using Presentation.Interfaces;
using Shared.Response;

namespace Presentation.Messages.Test
{
    public class DeleteTechnicianFromTestMessage : ISendMessage<RemoveTechnicianFromTestUseCase>
    {
        public DeleteTechnicianFromTestMessage(RemoveTechnicianFromTestUseCase useCase, IMsgService msgService, ILogger<ISendMessage<RemoveTechnicianFromTestUseCase>> logger) : base(useCase, msgService, logger)
        {
        }
         override public async Task Execute(){

            


            try
            {    
                //Subscribe to the message queue and execute the use case received idTest and idTechnician as parameters response is a GenericResponse   Task SubscribeAsync<TRequest, TResponse>(string subject, Func<TRequest, Task<TResponse>> messageHandler);

               
                
               await _msgService.SubscribeAsync<TestTechnicians, GenericResponse>("DeleteTechnicianFromTest",

               async (message) =>{
                return await _useCase.Execute(message.TestId, message.EmployeeId);

               });
                                         
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing messages");
            }
        }
    }
}