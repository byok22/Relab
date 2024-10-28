using Application.UseCases.Users;
using AutoMapper;
using Domain.Enums;
using Domain.Repositories;
using Shared.Dtos;

namespace Application.UseCases.TestRequests
{
    public class GetTestsRequestByStatusUseCase
    {
        private readonly IMapper _mapper;
        private readonly ITestRequestRepository _TestRequestRepository;
        private readonly GetTestOfTestRequestsUseCases _getTestOfTestRequestsUseCases;
        private GetUserByIdUseCase  _getUserByIdUseCase;
        
        public GetTestsRequestByStatusUseCase( ITestRequestRepository testRequestRepositor, GetUserByIdUseCase  getUserByIdUseCase, IMapper mapper, GetTestOfTestRequestsUseCases getTestOfTestRequestsUseCases)
        {
            _TestRequestRepository = testRequestRepositor;
            _getUserByIdUseCase = getUserByIdUseCase;           
            _mapper=mapper;
            _getTestOfTestRequestsUseCases = getTestOfTestRequestsUseCases;
        }
        public  async Task<List<TestRequestDto>> Execute(TestRequestsStatus status){         

            var dtos = await _TestRequestRepository.GetTestRequestsByStatus(status);
            var result = dtos;

           var updatedResult = await Task.WhenAll(result.Select(async testRequest =>
            {
                testRequest.CreatedBy = await _getUserByIdUseCase.Execute(testRequest.CreatedBy!=null?testRequest.CreatedBy.Id:0);
                return testRequest;
            }));
            var resultDto = _mapper.Map<List<TestRequestDto>>(updatedResult);
           /* foreach(var testRequest in resultDto){
                testRequest.Tests = await _getTestOfTestRequestsUseCases.Execute(testRequest.Id);
            }*/
            return resultDto.ToList();

    
        }
    }
}