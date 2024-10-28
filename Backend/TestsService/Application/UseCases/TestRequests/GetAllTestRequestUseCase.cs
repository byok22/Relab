
using Domain.Repositories;

using AutoMapper;
using Shared.Dtos;


namespace Application.UseCases.TestRequests
{
    public class GetAllTestRequestUseCase
    {
        private readonly IMapper _mapper;
        private readonly ITestRequestRepository _TestRequestRepository;
        private readonly GetTestOfTestRequestsUseCases _getTestOfTestRequestsUseCases;
        public GetAllTestRequestUseCase( ITestRequestRepository testRequestRepositor, IMapper mapper
            , GetTestOfTestRequestsUseCases getTestOfTestRequestsUseCases)
        {
            _TestRequestRepository = testRequestRepositor;
           
            _mapper=mapper;
            _getTestOfTestRequestsUseCases = getTestOfTestRequestsUseCases;
        }

        public  async Task<List<TestRequestDto>> Execute(){           
            var dtos = await _TestRequestRepository.GetAllAsync();
           
            var result = _mapper.Map<List<TestRequestDto>>(dtos);
             foreach(var testRequest in result){
                testRequest.Tests = await _getTestOfTestRequestsUseCases.Execute(testRequest.Id);
            }
            return result.ToList();

        }
    }
}