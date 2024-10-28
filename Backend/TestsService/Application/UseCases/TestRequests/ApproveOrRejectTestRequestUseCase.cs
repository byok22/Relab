

using AutoMapper;
using Domain.Models.TestRequests;
using Domain.Repositories;
using Shared.Dtos;
using Shared.Response;

namespace Application.UseCases.TestRequests
{
    public class ApproveOrRejectTestRequestUseCase
    {
       private readonly IMapper _mapper;
        private readonly ITestRequestRepository _TestRequestRepository;
        public ApproveOrRejectTestRequestUseCase( IMapper mapper, ITestRequestRepository genericRepository )
        {
           
            _mapper=mapper;
            _TestRequestRepository=genericRepository;
        }
        public  async Task<GenericResponse> Execute(int id, ChangeStatusTestRequest changeStatusTestRequest)
        {          
                       

            try{
                var testRequests = await _TestRequestRepository.AproveOrDennyTestRequest(id, changeStatusTestRequest);

                    return new GenericResponse{
                    IsSuccessful =true,
                    Message = $"Test Request {testRequests.Description} is {testRequests.Status}",
                };

            }catch(Exception ex){

                 return  new GenericResponse{
                 IsSuccessful =false,
                 Message = "Error Aprovee Tests "+ex.Message,
                };

            }
        
        
        }
    }
}