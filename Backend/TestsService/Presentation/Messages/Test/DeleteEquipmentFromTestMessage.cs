
using System.Text.Json;
using Application.UseCases.TestEquipmentsUseCases;
using Domain.Models.TestModels;
using Domain.Services;
using Presentation.Interfaces;
using Shared.Response;

namespace Presentation.Messages.Test
{
    public class DeleteEquipmentFromTestMessage : ISendMessage<RemoveEquipmentsFromTestUseCase>
    {
        public DeleteEquipmentFromTestMessage(RemoveEquipmentsFromTestUseCase useCase, IMsgService msgService, ILogger<ISendMessage<RemoveEquipmentsFromTestUseCase>> logger) : base(useCase, msgService, logger)
        {
        }
         override public async Task Execute(){

            


            try
            {    
                //Subscribe to the message queue and execute the use case received idTest and idEquipment as parameters response is a GenericResponse   Task SubscribeAsync<TRequest, TResponse>(string subject, Func<TRequest, Task<TResponse>> messageHandler);

               
                
               await _msgService.SubscribeAsync<TestEquipment, GenericResponse>("DeleteEquipmentFromTest",

               async (message) =>{
                return await _useCase.Execute(message.TestId, message.EquipmentId);

               });
                                         
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing messages");
            }
        }
    }
}