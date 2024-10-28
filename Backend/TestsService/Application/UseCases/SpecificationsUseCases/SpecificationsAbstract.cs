
using AutoMapper;
using Domain.Repositories;

namespace Application.UseCases.SpecificationsUseCases
{
    public abstract class SpecificationsAbstract
    {
        public readonly IMapper _mapper;
        public readonly ISpecificationsRepository _repository;

        public SpecificationsAbstract(ISpecificationsRepository repository, IMapper mapper)
        {
            _repository = repository;  
            _mapper = mapper;          
        }


    }
}