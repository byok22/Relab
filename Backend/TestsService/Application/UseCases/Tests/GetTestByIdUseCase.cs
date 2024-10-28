using Application.UseCases.AttachmentUseCases;
using Application.UseCases.TestChangeStatusTestUseCases;
using Application.UseCases.TestEquipmentsUseCases;
using Application.UseCases.TestGenericUpdateUseCases;
using Application.UseCases.TestSpecificationsUseCases;
using Application.UseCases.TestTechniciansUseCases;
using AutoMapper;
using Domain.Repositories;
using Shared.Dtos;

namespace Application.UseCases.Tests
{
    public class GetTestByIdUseCase
    {
        private readonly IMapper _mapper;      
        private readonly ITestRepository _testRepository; 
        private readonly GetAttachmentsFromTestUseCase _getAttachmentsFromTestUseCase;
        private readonly GetTechniciansFromTestsUseCase _getTechniciansFromTestsUseCase;
        private readonly TestSpecificationsFromTestUseCase _getSpecificationsFromTestUseCase;
        private readonly TestEquipmentsFromTestUseCase _getEquipmentsFromTestUseCase;
        private readonly ChangeStatusFromTestUseCase _changeStatusFromTestUseCase;
        private readonly GetGenericUpdatesFromTestUseCase _getUpdatesFromTestUseCase;
               public GetTestByIdUseCase( IMapper mapper, 
               ITestRepository testRepository, 
               GetAttachmentsFromTestUseCase getAttachmentsFromTestUseCase,
               GetTechniciansFromTestsUseCase getTechniciansFromTestsUseCase,
                TestSpecificationsFromTestUseCase getSpecificationsFromTestUseCase,
                TestEquipmentsFromTestUseCase getEquipmentsFromTestUseCase,
                ChangeStatusFromTestUseCase changeStatusFromTestUseCase,
                GetGenericUpdatesFromTestUseCase getUpdatesFromTestUseCase
               )
        {                      
            _mapper=mapper;
            _testRepository=testRepository;
            _getAttachmentsFromTestUseCase = getAttachmentsFromTestUseCase;
            _getTechniciansFromTestsUseCase = getTechniciansFromTestsUseCase;
            _getSpecificationsFromTestUseCase = getSpecificationsFromTestUseCase;
            _getEquipmentsFromTestUseCase = getEquipmentsFromTestUseCase;
            _changeStatusFromTestUseCase = changeStatusFromTestUseCase;
            _getUpdatesFromTestUseCase = getUpdatesFromTestUseCase;
            
        }

        public   async Task<TestDto> Execute(int id){    

            var test = await _testRepository.GetByIdAsync(id);
            var testDto = _mapper.Map<TestDto>(test);
            testDto.Attachments = await _getAttachmentsFromTestUseCase.Execute(id);
            testDto.Technicians = await _getTechniciansFromTestsUseCase.Execute(id);
            testDto.Specifications = await _getSpecificationsFromTestUseCase.Execute(id);
            testDto.Equipments = await _getEquipmentsFromTestUseCase.Execute(id);
            try{
               testDto.changeStatusTest = await _changeStatusFromTestUseCase.Execute(id);
            }
            catch (Exception e){
                //testDto.changeStatusTest = new ChangeStatusTestDto();
                
            }
            
            testDto.updates = await _getUpdatesFromTestUseCase.Execute(id);



            return testDto ;

        }
    }
}