using AutoMapper;
using Domain.Repositories;
using Shared.Dtos;

namespace Application.UseCases.SamplesUseCases
{
    public class GetSampleByIdUseCase : SamplesAbstract
    {
        public GetSampleByIdUseCase(ISamplesRepository repository, IMapper mapper) : base(repository, mapper)
        {
        }

          public  async Task<SampleDto> Execute(int id){         

            var dtos = await _repository.GetByIdAsync(id);                
            return _mapper.Map<SampleDto>(dtos);
        }
    }
}