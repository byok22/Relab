using AutoMapper;
using Domain.Models;
using Domain.Repositories;

namespace Application.UseCases.ChangeStatusTestUseCases
{
    public class GetChangeStatusTestByIdUseCase : ChangeStatusTestAbstract
    {
        public GetChangeStatusTestByIdUseCase(IChangeStatusTestRepository repository, IMapper mapper) : base(repository, mapper)
        {
        }

          public  async Task<ChangeStatusTest> Execute(int id){         

            var dtos = await _repository.GetByIdAsync(id);                
            return _mapper.Map<ChangeStatusTest>(dtos);
        }
    }
}