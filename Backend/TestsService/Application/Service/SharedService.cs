

using Domain.Models.Generics;
using Domain.Service.Shared;

namespace TestsService.Application.Service
{
    public class SharedService : ISharedService
    {
        public SharedService()
        {
       
            
        }
        public Task<List<DropDown>> GetStatuses()
        {
 
            List<DropDown> list = new List<DropDown>(){
                new DropDown
                {
                   Id="1",
                    Text= "Active"
                },
                new DropDown
                {
                   Id="2",
                    Text= "Expired"
                },
                new DropDown
                {
                   Id="3",
                    Text= "Not Asigned"
                },
                new DropDown
                {
                   Id="4",
                    Text= "Inactive"
                },
                 new DropDown
                {
                   Id="5",
                    Text= "Under repair"
                },                
                new DropDown
                {
                   Id="0",
                    Text= "All"
                }



            };
            return Task.FromResult(list);
            
        }
    }
}