using AutoMapper;
using Domain.Models;
using Domain.Repositories;
using Shared.Dtos;
using Shared.Response;

namespace Application.UseCases.ChangeStatusTestUseCases
{
    public class DeleteChangeStatusTestUseCase : ChangeStatusTestAbstract
    {
        public DeleteChangeStatusTestUseCase(IChangeStatusTestRepository repository, IMapper mapper) : base(repository, mapper)
        {
        }

        public async Task<GenericResponse> Execute(ChangeStatusTest ChangeStatusTestDto)
        {
            var ChangeStatusTest = _mapper.Map<ChangeStatusTest>(ChangeStatusTestDto);
            if (ChangeStatusTest == null)
                throw new ArgumentNullException(nameof(ChangeStatusTest));


           var result = await _repository.RemoveAsync(ChangeStatusTest);
            return new GenericResponse
            {
                IsSuccessful = result.id>0?true:false,
                Message = result.message
            };
            
        }
    }
    
}