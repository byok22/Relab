using Domain.Repositories;
using Shared.Response;
using AutoMapper;
using Shared.Dtos;
using Domain.Models;
using Domain.Models.TestRequests;
using Domain.Enums;
using Application.UseCases.Tests;


namespace Application.UseCases.TestRequests
{
    public class CreateTestsRequestUseCase
    {

        private readonly IMapper _mapper;
        private readonly ITestRequestRepository _TestRequestRepository;
        private readonly AddTestUseCase _addTestUseCase;
        public CreateTestsRequestUseCase( IMapper mapper, ITestRequestRepository genericRepository , AddTestUseCase addTestUseCase)
        {
           
            _mapper=mapper;
            _TestRequestRepository=genericRepository;
            _addTestUseCase = addTestUseCase;
        }
        public  async Task<GenericResponse> Execute(string descripcion, DateTime start, DateTime end, List<TestDto> tests, User user)
        {     
            var testsClass =      _mapper.Map<List<Test>>(tests)??new List<Test>();
           
            TestRequest result =  new TestRequest(){
                Description = descripcion,
                CreatedBy = user,
                Start = start,
                Tests = testsClass,
                 End = end,
                Active = true,
                Status = TestRequestsStatus.New,


            };
                               


            var testrequesClass = _mapper.Map<TestRequest>(result);
            
            try{
                var resultTest = await _TestRequestRepository.AddAsync(testrequesClass);
                var resultTestDto = _mapper.Map<TestRequestDto>(resultTest);

                if(resultTestDto.Id> 0 && resultTestDto.Tests!=null){
                    foreach(var test in tests){
                        test.idRequest = resultTestDto.Id;                            
                        await _addTestUseCase.Execute(test);
                    }
                }

                    return new GenericResponse{
                    IsSuccessful =true,
                    Message = "CreateTestRequest",
                    Id = resultTestDto.Id
                };

            }catch(Exception ex){

                 return  new GenericResponse{
                 IsSuccessful =false,
                 Message = "Error Created Tests "+ex.Message,
                };

            }
            


           

            

            


            //enviarlo al Repository


            //Send Notification


        
        }
    }
}