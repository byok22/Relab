using AutoMapper;
using Domain.Models;
using Domain.Repositories;
using Shared.Dtos;
using Shared.Response;

namespace Application.UseCases.SamplesUseCases
{
    public class AddSampleUseCase : SamplesAbstract
    {
        public AddSampleUseCase(ISamplesRepository repository, IMapper mapper) : base(repository, mapper)
        {
        }

        public  async Task<GenericResponse> Execute(SampleDto sampleDto){

            var sample = _mapper.Map<Samples>(sampleDto);

            if(sample != null){

                var att = await _repository.AddAsync(sample);
                return new GenericResponse(){
                    IsSuccessful = true,
                    Message = $"Sample Saved saved.",
                    Id = att.Id
                };
                
            }
            return new GenericResponse(){
                IsSuccessful = false,
                Message = $"Not Saved"
            };

        }
    }
}