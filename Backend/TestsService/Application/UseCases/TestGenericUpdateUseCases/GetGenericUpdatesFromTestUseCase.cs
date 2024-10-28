using Application.UseCases.Users;
using AutoMapper;
using Domain.Repositories;
using Shared.Dtos;

namespace Application.UseCases.TestGenericUpdateUseCases
{
    public class GetGenericUpdatesFromTestUseCase
    {
        private readonly ITestGenericUpdateRepository _repository;
        private readonly IMapper _mapper; 
        private readonly GetUserByIdUseCase _getUserByIdUseCase;

        public GetGenericUpdatesFromTestUseCase(
            ITestGenericUpdateRepository repository,
            IMapper mapper,
            GetUserByIdUseCase getUserByIdUseCase)
        {
            _repository = repository;
            _mapper = mapper;
            _getUserByIdUseCase = getUserByIdUseCase;
        }   

        public async Task<List<GenericUpdateDto>> Execute(int idTest)
        {
            var re = await _repository.GetGenericUpdatesByTestId(idTest);
            var genericUpdates =  _mapper.Map<List<GenericUpdateDto>>(re);
            foreach (var genericUpdate in genericUpdates)
            {
                if (genericUpdate.User != null && genericUpdate.User.Id != 0)
                genericUpdate.User = await _getUserByIdUseCase.Execute( genericUpdate.User.Id);
            }
            return genericUpdates;
        }


    }
}