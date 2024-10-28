using Domain.Models.Equipments;
using Domain.Services;
using Presentation.Interfaces;
using Application.UseCases.EquipmentsUseCases;

namespace Presentation.Messages.Equipments
{
    public class GetAllEquipmentsMessage : ISendMessage<GetAllEquipmentsUseCase>
    {
        public GetAllEquipmentsMessage(GetAllEquipmentsUseCase useCase, IMsgService msgService, ILogger<ISendMessage<GetAllEquipmentsUseCase>> logger) : base(useCase, msgService, logger)
        {
        }

        override  public async Task Execute()
        {
            try
            {           
                
               await _msgService.SubscribeAsync<List<Equipment>>("GetAllEquipments",

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