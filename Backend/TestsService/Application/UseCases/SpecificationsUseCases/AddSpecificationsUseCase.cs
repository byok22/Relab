using AutoMapper;
using Domain.Models;
using Shared.Dtos;
using Shared.Response;
using Domain.Repositories;

namespace Application.UseCases.SpecificationsUseCases
{
    public class AddSpecificationUseCase : SpecificationsAbstract
    {
        public AddSpecificationUseCase(ISpecificationsRepository repository, IMapper mapper) : base(repository, mapper)
        {
        }

        public  async Task<GenericResponse> Execute(SpecificationDto SpecificationDto){

            var Specification = _mapper.Map<Specification>(SpecificationDto);

            if(Specification != null){

                var att =await _repository.AddAsync(Specification);
                return new GenericResponse(){
                    IsSuccessful = true,
                    Message = $"Specification Saved saved.",
                    Id = att.Id,
                };
            }
            return new GenericResponse(){
                IsSuccessful = false,
                Message = $"Not Saved"
            };

        }
    }
}