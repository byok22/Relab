using AutoMapper;
using Domain.Enums;
using Domain.Models;
using Domain.Models.TestRequests;
using Domain.Repositories;
using Shared.Dtos;
using Shared.Response;

namespace Application.UseCases.TestRequests
{
    public class UpdateTestRequestUseCase
    {
        private readonly IMapper _mapper;
        private readonly ITestRequestRepository _TestRequestRepository;
        public UpdateTestRequestUseCase( IMapper mapper, ITestRequestRepository genericRepository )
        {
           
            _mapper=mapper;
            _TestRequestRepository=genericRepository;
        }
        public  async Task<GenericResponse> Execute(int id, string descripcion, DateTime start, DateTime end, List<TestDto> tests, User user)
        {     
            var testsClass = _mapper.Map<List<Test>>(tests)??new List<Test>();
           
            TestRequest result =  new TestRequest(){
                Id = id,
                Description = descripcion,
                CreatedBy = user,
                Start = start,
                Tests = testsClass,
                 End = end,
                Active = true,
                Status = TestRequestsStatus.New,


            };
                               


            var dto = _mapper.Map<TestRequestDto>(result);


            try{
                var testRequest = _mapper.Map<TestRequest>(dto);
                await _TestRequestRepository.UpdateAsync(testRequest);

                    return new GenericResponse{
                    IsSuccessful =true,
                    Message = "Updated Test Request",
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