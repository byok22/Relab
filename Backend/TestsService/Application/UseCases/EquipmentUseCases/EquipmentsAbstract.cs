
using AutoMapper;
using Domain.Repositories;

namespace Application.UseCases.EquipmentsUseCases
{
    public abstract class EquipmentsAbstract
    {
        public readonly IMapper _mapper;
        public readonly IEquipmentsRepository _repository;

        public EquipmentsAbstract(IEquipmentsRepository repository, IMapper mapper)
        {
            _repository = repository;  
            _mapper = mapper;          
        }


    }
}