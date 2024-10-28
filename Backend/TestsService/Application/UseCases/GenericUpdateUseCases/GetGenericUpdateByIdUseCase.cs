using AutoMapper;
using Shared.Dtos;
using Domain.Repositories;

namespace Application.UseCases.GenericUpdateUseCases
{
    public class GetGenericUpdateByIdUseCase : GenericUpdateAbstract
    {
        public GetGenericUpdateByIdUseCase(IGenericUpdateRepository repository, IMapper mapper) : base(repository, mapper)
        {
        }

          public  async Task<GenericUpdateDto> Execute(int id){         

            var dtos = await _repository.GetByIdAsync(id);                
            return _mapper.Map<GenericUpdateDto>(dtos);
        }
    }
}