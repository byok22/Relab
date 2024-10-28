using AutoMapper;
using Shared.Dtos;
using Domain.Repositories;

namespace Application.UseCases.SpecificationsUseCases
{
    public class GetSpecificationByIdUseCase : SpecificationsAbstract
    {
        public GetSpecificationByIdUseCase(ISpecificationsRepository repository, IMapper mapper) : base(repository, mapper)
        {
        }

          public  async Task<SpecificationDto> Execute(int id){         

            var dtos = await _repository.GetByIdAsync(id);                
            return _mapper.Map<SpecificationDto>(dtos);
        }
    }
}