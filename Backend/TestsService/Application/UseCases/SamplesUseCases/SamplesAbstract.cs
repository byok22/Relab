using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Repositories;

namespace Application.UseCases.SamplesUseCases
{
    public abstract class SamplesAbstract
    {
        public readonly IMapper _mapper;
        public readonly ISamplesRepository _repository;

        public SamplesAbstract(ISamplesRepository repository, IMapper mapper)
        {
            _repository = repository;  
            _mapper = mapper;          
        }


    }
}