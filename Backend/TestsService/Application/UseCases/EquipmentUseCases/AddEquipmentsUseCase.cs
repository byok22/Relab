using AutoMapper;
using Domain.Models;
using Shared.Dtos;
using Shared.Response;
using Domain.Repositories;
using Domain.Models.Equipments;

namespace Application.UseCases.EquipmentsUseCases
{
    public class AddEquipmentUseCase : EquipmentsAbstract
    {
        public AddEquipmentUseCase(IEquipmentsRepository repository, IMapper mapper) : base(repository, mapper)
        {
        }

        public  async Task<GenericResponse> Execute(Equipment equipment){
            

            if(equipment != null){

                var att = _repository.AddAsync(equipment);
                return new GenericResponse(){
                    IsSuccessful = true,
                    Message = $"Equipment Saved saved.",
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