using AutoMapper;
using Domain.Models;
using Domain.Repositories;
using Shared.Dtos;
using Shared.Response;

namespace Application.UseCases.SpecificationsUseCases
{
    public class DeleteSpecificationUseCase : SpecificationsAbstract
    {
        public DeleteSpecificationUseCase(ISpecificationsRepository repository, IMapper mapper) : base(repository, mapper)
        {
        }

        public async Task<GenericResponse> Execute(SpecificationDto specificationDto)
        {
            var specification = _mapper.Map<Specification>(specificationDto);
            if (specification == null)
                throw new ArgumentNullException(nameof(specification));


           var result = await _repository.RemoveAsync(specification);
            return new GenericResponse
            {
                IsSuccessful = result.id>0?true:false,
                Message = result.message
            };
            
        }
    }
    
}