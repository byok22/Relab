using AutoMapper;
using Domain.Models;
using Shared.Dtos;
using Shared.Response;
using Domain.Repositories;

namespace Application.UseCases.SpecificationsUseCases
{
    public class UpdateSpecificationUseCase : SpecificationsAbstract
    {
        public UpdateSpecificationUseCase(ISpecificationsRepository repository, IMapper mapper) : base(repository, mapper)
        {
        }
         public  async Task<GenericResponse> Execute(SpecificationDto SpecificationDto)
        {     
          
            


            var objs = _mapper.Map<Specification>(SpecificationDto);


            try{
                await _repository.UpdateAsync(objs);

                    return new GenericResponse{
                    IsSuccessful =true,
                    Message = "Update Specification"
                };

            }catch(Exception ex){

                 return  new GenericResponse{
                 IsSuccessful =false,
                 Message = "Error Update Specification "+ex.Message,
                };

            }
            


           

            

            


            //enviarlo al Repository


            //Send Notification


        
        }
    }
}