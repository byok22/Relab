using AutoMapper;
using Shared.Dtos;
using Domain.Repositories;
using Domain.Models.Equipments;

namespace Application.UseCases.EquipmentsUseCases
{
    public class GetEquipmentByIdUseCase : EquipmentsAbstract
    {
        public GetEquipmentByIdUseCase(IEquipmentsRepository repository, IMapper mapper) : base(repository, mapper)
        {
        }

          public  async Task<Equipment> Execute(int id){         

            var s = await _repository.GetByIdAsync(id);                
            return s;
        }
    }
}