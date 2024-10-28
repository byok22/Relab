using AutoMapper;
using Domain.Models;
using Domain.Repositories;
using Shared.Dtos;
using Shared.Response;

namespace Application.UseCases.SamplesUseCases
{
    public class UpdateSampleUseCase : SamplesAbstract
    {
        public UpdateSampleUseCase(ISamplesRepository repository, IMapper mapper) : base(repository, mapper)
        {
        }
         public  async Task<GenericResponse> Execute(SampleDto sampleDto)
        {     
          
            


            var objs = _mapper.Map<Samples>(sampleDto);


            try{
                await _repository.UpdateAsync(objs);

                    return new GenericResponse{
                    IsSuccessful =true,
                    Message = "Update Sample"
                };

            }catch(Exception ex){

                 return  new GenericResponse{
                 IsSuccessful =false,
                 Message = "Error Update Sample "+ex.Message,
                };

            }
            


           

            

            


            //enviarlo al Repository


            //Send Notification


        
        }
    }
}