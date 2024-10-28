using AutoMapper;
using Shared.Response;
using Domain.Repositories;
using Domain.Models.Generics;
using Shared.Dtos;
namespace Application.UseCases.GenericUpdateUseCases
{
    public class AddGenericUpdateUseCase : GenericUpdateAbstract
    {
        public AddGenericUpdateUseCase(IGenericUpdateRepository repository, IMapper mapper) : base(repository, mapper)
        {
        }

        public  async Task<GenericResponse> Execute(GenericUpdateDto GenericUpdateDto){

            var GenericUpdate = _mapper.Map<GenericUpdate>(GenericUpdateDto);

            if(GenericUpdate != null){

                var att = _repository.AddAsync(GenericUpdate);
                return new GenericResponse(){
                    IsSuccessful = true,
                    Message = $"GenericUpdate Saved saved.",
                };
            }
            return new GenericResponse(){
                IsSuccessful = false,
                Message = $"Not Saved"
            };

        }
    }
}