using Domain.Models.Generics;
using Domain.Services;
using Presentation.Interfaces;

namespace Presentation.Messages.DropDowns
{
    public class GetEquipmentsStatusDropDownMessage: ISendMessage<IDropDownsService>
    {
        public GetEquipmentsStatusDropDownMessage(IDropDownsService service, IMsgService msgService, ILogger<ISendMessage<IDropDownsService>> logger) : base(service, msgService, logger)
        {
        }
        override public async Task Execute(){

             try
            {           

               await _msgService.SubscribeAsync<List<DropDown>>("GetEquipmentStatus",

               async () =>{
                return await _useCase.GetStatus();

               });
               
                                         
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing messages");
            }
           
        }    
    }
}