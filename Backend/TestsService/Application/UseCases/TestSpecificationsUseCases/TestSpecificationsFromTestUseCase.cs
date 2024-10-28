

using AutoMapper;
using Domain.Repositories;
using Shared.Dtos;

namespace Application.UseCases.TestSpecificationsUseCases
{
    public class TestSpecificationsFromTestUseCase
    {
        private readonly ITestSpecificationsRepository _repository;
        private readonly IMapper _mapper;

        public TestSpecificationsFromTestUseCase(ITestSpecificationsRepository repository
        , IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<SpecificationDto>> Execute(int idTest)
        {
            var re = await _repository.GetSpecificationsByTestId(idTest);
            var specifications =  _mapper.Map<List<SpecificationDto>>(re);
            return specifications;
        }
        
    }
}