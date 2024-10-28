using AutoMapper;
using Domain.Models.Generics;
using Shared.Dtos;
using Shared.Response;
using Domain.Repositories;

namespace Application.UseCases.GenericUpdateUseCases
{
    public class UpdateGenericUpdateUseCase : GenericUpdateAbstract
    {
        public UpdateGenericUpdateUseCase(IGenericUpdateRepository repository, IMapper mapper) : base(repository, mapper)
        {
        }
         public  async Task<GenericResponse> Execute(GenericUpdateDto GenericUpdateDto)
        {     
          
            


            var objs = _mapper.Map<GenericUpdate>(GenericUpdateDto);


            try{
                await _repository.UpdateAsync(objs);

                    return new GenericResponse{
                    IsSuccessful =true,
                    Message = "Update GenericUpdate"
                };

            }catch(Exception ex){

                 return  new GenericResponse{
                 IsSuccessful =false,
                 Message = "Error Update GenericUpdate "+ex.Message,
                };

            }
            


           

            

            


            //enviarlo al Repository


            //Send Notification


        
        }
    }
}