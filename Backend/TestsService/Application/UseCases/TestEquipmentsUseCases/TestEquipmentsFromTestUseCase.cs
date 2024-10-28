

using AutoMapper;
using Domain.Models.Equipments;
using Domain.Repositories;

namespace Application.UseCases.TestEquipmentsUseCases
{
    public class TestEquipmentsFromTestUseCase
    {
        private readonly ITestEquipmentsRepository _repository;
        private readonly IMapper _mapper;

        public TestEquipmentsFromTestUseCase(ITestEquipmentsRepository repository
        , IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<Equipment>> Execute(int idTest)
        {
            var re = await _repository.GetEquipmentByTestId(idTest);
            var Equipments =  _mapper.Map<List<Equipment>>(re);
            return Equipments;
        }
        
    }
}