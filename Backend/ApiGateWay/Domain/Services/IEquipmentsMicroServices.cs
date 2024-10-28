using Domain.Models.Equipments;
using Domain.Models.Generics;
using Shared.Response;

namespace Domain.Services
{
    public interface IEquipmentsMicroServices
    {
        Task<GenericResponse> AddEquipment(Equipment testDto);
        Task<List<Equipment>> GetAllEquipments();
        Task<Equipment> GetEquipmentById(int id);
       
        Task<GenericResponse> PatchEquipment(Equipment testDto);      
        Task<List<DropDown>> GetEquipmentsStatus();         

        Task<GenericResponse> RemoveEquipment(int id);
    }
}