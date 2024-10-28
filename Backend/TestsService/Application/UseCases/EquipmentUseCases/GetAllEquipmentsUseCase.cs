using Domain.Models.Equipments;
using Domain.Repositories;

namespace  Application.UseCases.EquipmentsUseCases
{
    public class GetAllEquipmentsUseCase
    {
        private readonly IEquipmentsRepository _EquipmentRepository;
        public GetAllEquipmentsUseCase( IEquipmentsRepository EquipmentRepositor)
        {
            _EquipmentRepository = EquipmentRepositor;
           
            
        }

        public  async Task<List<Equipment>> Execute(){           
            var dtos = await _EquipmentRepository.GetAllAsync();
            
            return dtos.ToList();

        }
    }
}