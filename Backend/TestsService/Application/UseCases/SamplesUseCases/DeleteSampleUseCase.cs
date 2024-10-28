using AutoMapper;
using Domain.Models;
using Domain.Repositories;
using Shared.Dtos;
using Shared.Response;

namespace Application.UseCases.SamplesUseCases
{
    public class DeleteSampleUseCase : SamplesAbstract
    {
        public DeleteSampleUseCase(ISamplesRepository repository, IMapper mapper) : base(repository, mapper)
        {
        }

        public async Task<GenericResponse> Execute(SampleDto sampleDto)
        {
            var sample = _mapper.Map<Samples>(sampleDto);
            if (sample == null)
                throw new ArgumentNullException(nameof(sample));


           var result = await _repository.RemoveAsync(sample);
            return new GenericResponse
            {
                IsSuccessful = result.id>0?true:false,
                Message = result.message
            };
            
        }
    }
    
}