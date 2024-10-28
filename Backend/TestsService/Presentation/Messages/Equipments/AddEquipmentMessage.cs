using Application.UseCases.EquipmentsUseCases;
using Domain.Models.Equipments;
using Domain.Services;
using Presentation.Interfaces;
using Shared.Response;

namespace Presentation.Messages.Equipments
{
    public class AddEquipmentMessage : ISendMessage<AddEquipmentUseCase>
    {
        public AddEquipmentMessage(AddEquipmentUseCase useCase, IMsgService msgService, ILogger<ISendMessage<AddEquipmentUseCase>> logger) : base(useCase, msgService, logger)
        {
        }
        override public async Task Execute(){

             try
            {           

               await _msgService.SubscribeAsync<Equipment ,GenericResponse>("AddEquipment", async (equipment) =>{

               
                return await _useCase.Execute(equipment);

               });
                                         
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing messages");
            }
           
        }    
    }
}