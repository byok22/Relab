using Domain.Models.Generics;
using Domain.Service.Shared;
using Domain.Services;

namespace TestsService.Application.Service
{
    public class DropDownsService: IDropDownsService
    {
        private readonly ISharedService _sharedService;
      
        public DropDownsService(ISharedService sharedService)
        {
            _sharedService = sharedService;           
        }
        public async Task<List<DropDown>> GetStatus()
        {
            var status = await _sharedService.GetStatuses();
           return status;
        }
    }
}