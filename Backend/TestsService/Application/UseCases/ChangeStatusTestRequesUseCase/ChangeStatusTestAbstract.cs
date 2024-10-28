using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Repositories;

namespace Application.UseCases.ChangeStatusTestUseCases
{
    public abstract class ChangeStatusTestAbstract
    {
        public readonly IMapper _mapper;
        public readonly IChangeStatusTestRepository _repository;

        public ChangeStatusTestAbstract(IChangeStatusTestRepository repository, IMapper mapper)
        {
            _repository = repository;  
            _mapper = mapper;          
        }


    }
}