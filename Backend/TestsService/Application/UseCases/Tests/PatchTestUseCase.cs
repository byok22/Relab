using Shared.Dtos;
using AutoMapper;
using Shared.Response;
using Domain.Repositories;
using Domain.Models;
using Application.UseCases.SamplesUseCases;
using Application.UseCases.AttachmentUseCases;
using Application.UseCases.TestAttachmentsUseCases;
using Application.UseCases.SpecificationsUseCases;
using Application.UseCases.TestSpecificationsUseCases;
using Application.UseCases.TestTechniciansUseCases;
using Application.UseCases.TestEquipmentUseCases;
using Application.UseCases.TestEquipmentsUseCases;

namespace Application.UseCases.Testing
{
    public class PatchTestUseCase
    {
        private readonly IMapper _mapper;       
        private readonly ITestRepository _testRepository;
        private readonly AddSampleUseCase _addSampleUseCase;
        private readonly UpdateSampleUseCase _updateSampleUseCase;
        private readonly AddAttachmentUseCase   _addAttachmentUseCase;
        private readonly UpdateAttachmentUseCase _updateAttachmentUseCase;
        private readonly AsignAttachmentToTestUseCase _asignAttachmentToTestUseCase;
        private readonly RemoveAttachmentUseCase _removeAttachmentUseCase;
        private readonly RemoveAttachmentFromTestUseCase _removeAttachmentFromTestUseCase;
        private readonly RemoveTechnicianFromTestUseCase _removeTechnicianFromTestUseCase;
        private readonly AsignTechnicianToTestUseCase _asignTechnicianToTestUseCase;
        private readonly AddSpecificationUseCase _addSpecificationUseCase;
        private readonly AsignEquipmentToTestUseCase    _asignEquipmentToTestUseCase;
        private readonly AsignSpecificationToTestUseCase _asignSpecificationToTestUseCase;
        private readonly RemoveEquipmentsFromTestUseCase _removeEquipmentsFromTestUseCase;
        //Get Technicianas from test
        private readonly GetTechniciansFromTestsUseCase _getTechniciansFromTestUseCase;
        //get equipment from test
        private readonly TestEquipmentsFromTestUseCase _getEquipmentsFromTestUseCase;

               public PatchTestUseCase( 
            IMapper mapper, ITestRepository testRepository
            , AddSampleUseCase addSampleUseCase
            , UpdateSampleUseCase updateSampleUseCase
            , AddAttachmentUseCase addAttachmentUseCase
            , UpdateAttachmentUseCase updateAttachmentUseCase
            , AsignAttachmentToTestUseCase asignAttachmentToTestUseCase
            ,RemoveAttachmentUseCase removeAttachmentUseCase
            ,RemoveAttachmentFromTestUseCase removeAttachmentFromTestUseCase
            ,RemoveTechnicianFromTestUseCase removeTechnicianFromTestUseCase
            ,AsignTechnicianToTestUseCase asignTechnicianToTestUseCase
            ,AddSpecificationUseCase addSpecificationUseCase
            ,AsignSpecificationToTestUseCase asignSpecificationToTestUseCase
            ,AsignEquipmentToTestUseCase asignEquipmentToTestUseCase
            ,RemoveEquipmentsFromTestUseCase removeEquipmentsFromTestUseCase
            ,GetTechniciansFromTestsUseCase getTechniciansFromTestUseCase,
            TestEquipmentsFromTestUseCase getEquipmentsFromTestUseCase
            
            )
        {                      
            _mapper=mapper;
            _testRepository=testRepository;
            _addSampleUseCase = addSampleUseCase;
            _updateSampleUseCase = updateSampleUseCase;
            _addAttachmentUseCase = addAttachmentUseCase;
            _updateAttachmentUseCase = updateAttachmentUseCase;
            _asignAttachmentToTestUseCase = asignAttachmentToTestUseCase;
            _removeAttachmentUseCase = removeAttachmentUseCase;
            _removeAttachmentFromTestUseCase = removeAttachmentFromTestUseCase;
            _removeTechnicianFromTestUseCase = removeTechnicianFromTestUseCase;
            _asignTechnicianToTestUseCase = asignTechnicianToTestUseCase;
            _addSpecificationUseCase = addSpecificationUseCase;
            _asignSpecificationToTestUseCase = asignSpecificationToTestUseCase;
            _asignEquipmentToTestUseCase = asignEquipmentToTestUseCase;
            _getTechniciansFromTestUseCase = getTechniciansFromTestUseCase;
            _removeEquipmentsFromTestUseCase = removeEquipmentsFromTestUseCase;
            _getEquipmentsFromTestUseCase = getEquipmentsFromTestUseCase;

        }

        public  async Task<GenericResponse> Execute(TestDto testDto){      

            var test = _mapper.Map<Test>(testDto);
            try
            {
                var existingTest1 = await _testRepository.GetByIdAsync(testDto.Id);
                if (existingTest1 == null)
                {
                    return new GenericResponse
                    {
                        Message = "Test not found",
                        IsSuccessful = false
                    };
                }
                var existingTest = _mapper.Map<TestDto>(existingTest1);
                var technicias = await _getTechniciansFromTestUseCase.Execute(existingTest.Id);
                var equipments = await _getEquipmentsFromTestUseCase.Execute(existingTest.Id);
                existingTest.Equipments = equipments;
                existingTest.Technicians = technicias;
                existingTest = updateTestGeneralFields(existingTest, testDto);
                existingTest =await UpdateSample(existingTest, testDto);
                existingTest =await UpdateProfile(existingTest, testDto);
                existingTest = await UpdateAttachments(existingTest, testDto);
                existingTest = await UpdateTechnicians(existingTest, testDto);
                existingTest = await UpdateSpecs(existingTest, testDto);
                existingTest = await UpdateEquipments(existingTest, testDto);
                var testChanged = _mapper.Map<Test>(existingTest);

               
                await _testRepository.UpdateAsync(testChanged);

                return new GenericResponse
                {
                    Message = "Updated successfully",
                    IsSuccessful = true
                };
            }
            catch (Exception ex)
            {
                return new GenericResponse
                {
                    Message = "Error: " + ex.Message,
                    IsSuccessful = false
                };
            }
        }
        public async Task AsignAttachmentToTest(int testId, List<AttachmentDto>? attachments)
        {
            if (attachments != null)
            {
                foreach (var attachment in attachments)
                {
                    await _asignAttachmentToTestUseCase.Execute(testId, attachment.Id);
                }
            }
        }

        public TestDto  updateTestGeneralFields(TestDto existingTest, TestDto testDto)
        {
            existingTest.Name = testDto.Name;
            existingTest.Description = testDto.Description;
            existingTest.Start = testDto.Start;
            existingTest.End = testDto.End;
            existingTest.SpecialInstructions = testDto.SpecialInstructions;            
            existingTest.Status = testDto.Status;            
            existingTest.LastUpdatedMessage = testDto.LastUpdatedMessage;
            existingTest.idRequest = testDto.idRequest;
            existingTest.Enginner = testDto.Enginner;           
            return existingTest;
        }

        public async Task<TestDto> UpdateSample(TestDto existingTest, TestDto testDto)
        {
                if((existingTest.Samples == null || existingTest.Samples.Id == 0) &&
                    testDto.Samples != null && testDto.Samples.Size > 0)                 
                {
                    
                    var sample = await _addSampleUseCase.Execute(testDto.Samples);
                    testDto.Samples.Id = sample.Id??0; 
                    existingTest.Samples = testDto.Samples?? new SampleDto();
                }else if(existingTest.Samples != null 
                            && testDto.Samples != null && testDto.Samples.Id > 0                            
                        )
                {
                    await _updateSampleUseCase.Execute(testDto.Samples);
                    existingTest.Samples = testDto.Samples;
                }
            return existingTest;
        }

        public async Task<TestDto> UpdateProfile(TestDto existingTest, TestDto testDto)
        {
                if((existingTest.Profile == null || existingTest.Profile.Id == 0) && testDto.Profile != null && testDto.Profile.Name != "")
                {
                    var profile = await _addAttachmentUseCase.Execute(testDto.Profile);
                    testDto.Profile.Id = profile.Id??0;
                    existingTest.Profile = testDto.Profile;

                }else if(   
                            existingTest.Profile != null && existingTest.Profile.Id > 0 
                            && testDto.Profile != null && testDto.Profile.Name != ""
                            && existingTest.Profile.Name != testDto.Profile.Name
                        )
                {
                    
                    var profile = await _updateAttachmentUseCase.Execute(testDto.Profile);
                    existingTest.Profile = testDto.Profile;
                    existingTest.Profile.Id = profile.Id??0;

                }

            return existingTest;
        }

        public async Task<TestDto> UpdateAttachments(TestDto existingTest, TestDto testDto)
        {
            // Check if attachments are the same for both testDto and existingTest, do nothing if they are equal
            if 
                (
                    testDto.Attachments != null 
                    && existingTest.Attachments != null 
                    && testDto.Attachments.Count == existingTest.Attachments.Count 
                    && testDto.Attachments.All(a => existingTest.Attachments.Any(ea => ea.Id == a.Id))
                )
            {
                return existingTest;
            }

            if (testDto.Attachments != null)
                {
                
                        foreach(var attachment in existingTest.Attachments){
                            if(!testDto.Attachments.Any(t => t.Id == attachment.Id)){
                                await _removeAttachmentUseCase.Execute(attachment);
                                await _removeAttachmentFromTestUseCase.Execute(existingTest.Id, attachment.Id);
                                continue;
                            }
                            if(testDto.Attachments.Any(t => t.Id == attachment.Id && t.Name != attachment.Name)){
                                await _updateAttachmentUseCase.Execute(attachment);
                                continue;
                            }                                            
                    }              

                    foreach (var attachment in testDto.Attachments)
                    {
                        if (attachment.Id == 0)
                        {
                            var newAttachment = await _addAttachmentUseCase.Execute(attachment);
                            attachment.Id = newAttachment.Id ?? 0;
                            
                        }                   
                    }               
                return existingTest;
            }
            return existingTest;
        }
   
        public async Task<TestDto> UpdateTechnicians(TestDto existingTest, TestDto testDto)
        {
            if (testDto.Technicians != null)
            {
                foreach (var tech in existingTest.Technicians)
                {
                    if (!testDto.Technicians.Any(t => t.Id == tech.Id))
                    {
                        await _removeTechnicianFromTestUseCase.Execute(existingTest.Id, tech.Id);
                    }
                }

                foreach (var tech in testDto.Technicians)
                {
                    if (!existingTest.Technicians.Any(t => t.Id == tech.Id))
                    {
                        await _asignTechnicianToTestUseCase.Execute(existingTest.Id, tech.Id);
                    }
                }
            }
            existingTest.Technicians = testDto.Technicians;
            return existingTest;
        }
        public async Task<TestDto> UpdateSpecs(TestDto existingTest, TestDto testDto)
        {
            if (testDto.Specifications != null)
            {
                foreach (var spec in testDto.Specifications)
                {
                    if (spec.Id == 0)
                    {
                        var newSpec = await _addSpecificationUseCase.Execute(spec);
                        spec.Id = newSpec.Id ?? 0;
                        await _asignSpecificationToTestUseCase.Execute(existingTest.Id, spec.Id);
                    }                
                }
            }
            existingTest.Specifications = testDto.Specifications;
            return existingTest;
        }
        public async Task<TestDto> UpdateEquipments(TestDto existingTest, TestDto testDto)
        {
            //check if equipmests chaged and remove all from test
            if (testDto.Equipments != null)
            {
                foreach (var equipment in existingTest.Equipments)
                {
                    await _removeEquipmentsFromTestUseCase.Execute(existingTest.Id, equipment.Id);
                }
                foreach (var equipment in testDto.Equipments)
                {
                    await _asignEquipmentToTestUseCase.Execute(existingTest.Id, equipment.Id);
                }
            }
            existingTest.Equipments = testDto.Equipments;
            return existingTest;
        }
    }
}