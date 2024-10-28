using Domain.Models.Equipments;
using Domain.Models.TestModels;
using Shared.Dtos;
using Shared.Response;

namespace Domain.Repositories
{
    public interface ITestEquipmentsRepository
    {
        public Task<DBResponse> AssignEquipmentToTest(TestEquipment testEquipment);   
         public Task<DBResponse> RemoveEquipmentFromTest(TestEquipment testEquipment);
         public Task<List<Equipment>> GetEquipmentByTestId(int idTest);
    }
}