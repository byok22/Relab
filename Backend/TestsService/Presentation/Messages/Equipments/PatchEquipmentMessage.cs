using Application.UseCases.EquipmentsUseCases;
using Domain.Models.Equipments;
using Domain.Services;
using Presentation.Interfaces;
using Shared.Response;

namespace Presentation.Messages.Equipments
{
    public class PatchEquipmentMessage : ISendMessage<UpdateEquipmentUseCase>
    {
        public PatchEquipmentMessage(UpdateEquipmentUseCase useCase, IMsgService msgService, ILogger<ISendMessage<UpdateEquipmentUseCase>> logger) : base(useCase, msgService, logger)
        {
        }

         override public async Task Execute(){

             try
            {           

               await _msgService.SubscribeAsync<Equipment ,GenericResponse>("PatchEquipment",

               async (Equipment) =>{
                return await _useCase.Execute(Equipment);

               });
                                         
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing messages");
            }
           
        }     
    }
}