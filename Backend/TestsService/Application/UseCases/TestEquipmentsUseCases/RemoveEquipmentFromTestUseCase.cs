using Domain.Models.TestModels;
using Domain.Repositories;
using Shared.Response;

namespace Application.UseCases.TestEquipmentsUseCases
{
    public class RemoveEquipmentsFromTestUseCase
    {
        private readonly ITestEquipmentsRepository _repository;
        public RemoveEquipmentsFromTestUseCase(ITestEquipmentsRepository repository)
        {          
            _repository = repository;           
        }        
        public async Task<GenericResponse> Execute(int idTest, int idEquipments)
        {
            var testEquipments = new TestEquipment(){
                Id = 0,
                EquipmentId = idEquipments,
                TestId = idTest
            };
            var response = await _repository.RemoveEquipmentFromTest(testEquipments);
            return new GenericResponse(){
                IsSuccessful  = response.id>0?true:false,
                Message = response.message
            };

        }
        
    }
}