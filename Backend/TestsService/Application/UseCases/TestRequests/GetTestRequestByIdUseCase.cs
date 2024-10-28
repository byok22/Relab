using Application.UseCases.Users;
using AutoMapper;
using Domain.Repositories;
using Shared.Dtos;

namespace Application.UseCases.TestRequests
{
    public class GetTestRequestByIdUseCase
    {
        private readonly IMapper _mapper;
        private readonly ITestRequestRepository _TestRequestRepository;
        private readonly GetTestOfTestRequestsUseCases _getTestOfTestRequestsUseCases;
        private GetUserByIdUseCase  _getUserByIdUseCase;
        public GetTestRequestByIdUseCase( ITestRequestRepository testRequestRepositor, GetUserByIdUseCase  getUserByIdUseCase, IMapper mapper, GetTestOfTestRequestsUseCases getTestOfTestRequestsUseCases)
        {
            _TestRequestRepository = testRequestRepositor;
            _getUserByIdUseCase = getUserByIdUseCase;
            _getTestOfTestRequestsUseCases = getTestOfTestRequestsUseCases;
           
            _mapper=mapper;
        }
        public  async Task<TestRequestDto> Execute(int id){         

            var result = await _TestRequestRepository.GetByIdAsync(id);
            var dto = _mapper.Map<TestRequestDto>(result);
            dto.CreatedBy = await _getUserByIdUseCase.Execute(result.CreatedBy!=null?result.CreatedBy.Id:0);
            dto.Tests = await _getTestOfTestRequestsUseCases.Execute(result.Id);


            return dto;
        }
    }
}