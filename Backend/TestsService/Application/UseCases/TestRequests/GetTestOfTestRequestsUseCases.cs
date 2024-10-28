

using Application.UseCases.AttachmentUseCases;
using Application.UseCases.TestChangeStatusTestUseCases;
using Application.UseCases.TestEquipmentsUseCases;
using Application.UseCases.TestGenericUpdateUseCases;
using Application.UseCases.TestSpecificationsUseCases;
using Application.UseCases.TestTechniciansUseCases;
using AutoMapper;
using Domain.Repositories;
using Shared.Dtos;

namespace Application.UseCases.TestRequests
{
    public class GetTestOfTestRequestsUseCases
    {
        private readonly IMapper _mapper;
        private readonly ITestRequestRepository _testRequestRepository;
          private readonly GetAttachmentsFromTestUseCase _getAttachmentsFromTestUseCase;
        private readonly GetTechniciansFromTestsUseCase _getTechniciansFromTestsUseCase;
        private readonly TestSpecificationsFromTestUseCase _getSpecificationsFromTestUseCase;
        private readonly TestEquipmentsFromTestUseCase _getEquipmentsFromTestUseCase;
        private readonly ChangeStatusFromTestUseCase _changeStatusFromTestUseCase;
        private readonly GetGenericUpdatesFromTestUseCase _getUpdatesFromTestUseCase;

        public GetTestOfTestRequestsUseCases(IMapper mapper, ITestRequestRepository testRequestRepository
            , GetAttachmentsFromTestUseCase getAttachmentsFromTestUseCase,
            GetTechniciansFromTestsUseCase getTechniciansFromTestsUseCase,
            TestSpecificationsFromTestUseCase getSpecificationsFromTestUseCase,
            TestEquipmentsFromTestUseCase getEquipmentsFromTestUseCase,
            ChangeStatusFromTestUseCase changeStatusFromTestUseCase,
            GetGenericUpdatesFromTestUseCase getUpdatesFromTestUseCase)
        {
            _mapper = mapper;
            _testRequestRepository = testRequestRepository;
            _getAttachmentsFromTestUseCase = getAttachmentsFromTestUseCase;
            _getTechniciansFromTestsUseCase = getTechniciansFromTestsUseCase;
            _getSpecificationsFromTestUseCase = getSpecificationsFromTestUseCase;
            _getEquipmentsFromTestUseCase = getEquipmentsFromTestUseCase;
            _changeStatusFromTestUseCase = changeStatusFromTestUseCase;
            _getUpdatesFromTestUseCase = getUpdatesFromTestUseCase;
        }

        public async Task<List<TestDto>> Execute(int idTestRequest)
        {
            var dtos = await _testRequestRepository.GetTestOfTestRequest(idTestRequest);
            var result = _mapper.Map<List<TestDto>>(dtos);
            
           foreach (var testDto in result)
            {
                var id = testDto.Id;
               testDto.Attachments = await _getAttachmentsFromTestUseCase.Execute(id);
                testDto.Technicians = await _getTechniciansFromTestsUseCase.Execute(id);
                testDto.Specifications = await _getSpecificationsFromTestUseCase.Execute(id);
                testDto.Equipments = await _getEquipmentsFromTestUseCase.Execute(id);
                try{
                      testDto.changeStatusTest = await _changeStatusFromTestUseCase.Execute(id);
                }
                catch(Exception e){
                    //testDto.changeStatusTest = new ChangeStatusTestDto();
                }
              
                testDto.updates = await _getUpdatesFromTestUseCase.Execute(id);
            }
            



            return result.ToList();
        }

        
    }
}