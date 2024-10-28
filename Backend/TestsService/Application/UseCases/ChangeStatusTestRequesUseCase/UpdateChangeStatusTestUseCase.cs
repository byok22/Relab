using AutoMapper;
using Domain.Models;
using Domain.Repositories;
using Shared.Response;

namespace Application.UseCases.ChangeStatusTestUseCases
{
    public class UpdateChangeStatusTestUseCase : ChangeStatusTestAbstract
    {
        public UpdateChangeStatusTestUseCase(IChangeStatusTestRepository repository, IMapper mapper) : base(repository, mapper)
        {
        }
         public  async Task<GenericResponse> Execute(ChangeStatusTest ChangeStatusTestDto)
        {                           
            var objs = _mapper.Map<ChangeStatusTest>(ChangeStatusTestDto);
            try{
                await _repository.UpdateAsync(objs);

                    return new GenericResponse{
                    IsSuccessful =true,
                    Message = "Update ChangeStatusTest"
                };

            }catch(Exception ex){

                 return  new GenericResponse{
                 IsSuccessful =false,
                 Message = "Error Update ChangeStatusTest "+ex.Message,
                };

            }
            

            //enviarlo al Repository


            //Send Notification


        
        }
    }
}