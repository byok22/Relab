using AutoMapper;
using Domain.Models;
using Domain.Models.Equipments;
using Domain.Repositories;
using Shared.Dtos;
using Shared.Response;

namespace Application.UseCases.EquipmentsUseCases
{
    public class DeleteEquipmentUseCase : EquipmentsAbstract
    {
        public DeleteEquipmentUseCase(IEquipmentsRepository repository, IMapper mapper) : base(repository, mapper)
        {
        }

        public async Task<GenericResponse> Execute(Equipment equipment)
        {
            
            if (equipment == null)
                throw new ArgumentNullException(nameof(equipment));


           var result = await _repository.RemoveAsync(equipment);
            return new GenericResponse
            {
                IsSuccessful = result.id>0?true:false,
                Message = result.message
            };
            
        }
    }
    
}