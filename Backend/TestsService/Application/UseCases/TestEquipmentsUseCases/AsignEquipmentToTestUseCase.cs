using Domain.Models.TestModels;
using Domain.Repositories;
using Shared.Response;

namespace Application.UseCases.TestEquipmentUseCases
{
    public class AsignEquipmentToTestUseCase
    {
        
        private readonly ITestEquipmentsRepository _repository;


        public AsignEquipmentToTestUseCase(ITestEquipmentsRepository repository)
        {
          
            _repository = repository;
           
        }
        
        public async Task<GenericResponse> Execute(int idTest, int idEquipment)
        {
            var testEquipment = new TestEquipment(){
                Id = 0,
                EquipmentId = idEquipment,
                TestId = idTest
            };
            var response = await _repository.AssignEquipmentToTest(testEquipment);
            return new GenericResponse(){
                IsSuccessful  = response.id>0?true:false,
                Message = response.message
            };

        }
    }
}