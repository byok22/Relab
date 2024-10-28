
using AutoMapper;
using Domain.Repositories;

namespace Application.UseCases.EmployeeUseCases
{
    public abstract class EmployeeAbstract
    {
        public readonly IMapper _mapper;
        public readonly IEmployeeRepository _repository;

        public EmployeeAbstract(IEmployeeRepository repository, IMapper mapper)
        {
            _repository = repository;  
            _mapper = mapper;          
        }


    }
}