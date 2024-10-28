using AutoMapper;
using Domain.Models;
using Shared.Dtos;
using Shared.Response;
using Domain.Repositories;
using Domain.Models.Equipments;

namespace Application.UseCases.EquipmentsUseCases
{
    public class UpdateEquipmentUseCase : EquipmentsAbstract
    {
        public UpdateEquipmentUseCase(IEquipmentsRepository repository, IMapper mapper) : base(repository, mapper)
        {
        }
         public  async Task<GenericResponse> Execute(Equipment objs)
        {     
          
            


            


            try{
                await _repository.UpdateAsync(objs);

                    return new GenericResponse{
                    IsSuccessful =true,
                    Message = "Update Equipment"
                };

            }catch(Exception ex){

                 return  new GenericResponse{
                 IsSuccessful =false,
                 Message = "Error Update Equipment "+ex.Message,
                };

            }
            


           

            

            


            //enviarlo al Repository


            //Send Notification


        
        }
    }
}