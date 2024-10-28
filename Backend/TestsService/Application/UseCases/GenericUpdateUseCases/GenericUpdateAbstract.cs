
using AutoMapper;
using Domain.Repositories;

namespace Application.UseCases.GenericUpdateUseCases
{
    public abstract class GenericUpdateAbstract
    {
        public readonly IMapper _mapper;
        public readonly IGenericUpdateRepository _repository;

        public GenericUpdateAbstract(IGenericUpdateRepository repository, IMapper mapper)
        {
            _repository = repository;  
            _mapper = mapper;          
        }


    }
}