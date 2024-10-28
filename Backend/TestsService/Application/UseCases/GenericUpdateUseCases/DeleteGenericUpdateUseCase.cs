using AutoMapper;
using Domain.Repositories;
using Shared.Dtos;
using Shared.Response;
using Domain.Models.Generics;

namespace Application.UseCases.GenericUpdateUseCases
{
    public class DeleteGenericUpdateUseCase : GenericUpdateAbstract
    {
        public DeleteGenericUpdateUseCase(IGenericUpdateRepository repository, IMapper mapper) : base(repository, mapper)
        {
        }

        public async Task<GenericResponse> Execute(GenericUpdateDto GenericUpdateDto)
        {
            var GenericUpdate = _mapper.Map<GenericUpdate>(GenericUpdateDto);
            if (GenericUpdate == null)
                throw new ArgumentNullException(nameof(GenericUpdate));


           var result = await _repository.RemoveAsync(GenericUpdate);
            return new GenericResponse
            {
                IsSuccessful = result.id>0?true:false,
                Message = result.message
            };
            
        }
    }
    
}