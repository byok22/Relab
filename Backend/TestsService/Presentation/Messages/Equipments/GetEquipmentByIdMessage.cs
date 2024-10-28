using Presentation.Interfaces;
using Application.UseCases.EquipmentsUseCases;
using Domain.Services;
using Domain.Models.Equipments;


namespace Presentation.Messages.Equipments
{
    public class GetEquipmentByIdMessage : ISendMessage<GetEquipmentByIdUseCase>
    {
        public GetEquipmentByIdMessage(GetEquipmentByIdUseCase useCase, IMsgService msgService, ILogger<ISendMessage<GetEquipmentByIdUseCase>> logger) : base(useCase, msgService, logger)
        {
        }

         override public async Task Execute(){

            try
            {           
                
               await _msgService.SubscribeAsync<int, Equipment>("GetEquipmentById",

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