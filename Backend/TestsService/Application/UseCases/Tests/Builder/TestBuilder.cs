using Application.UseCases.AttachmentUseCases;
using Application.UseCases.ChangeStatusTestUseCases;
using Application.UseCases.EmployeeUseCases;
using Application.UseCases.SamplesUseCases;
using Application.UseCases.SpecificationsUseCases;
using Domain.Enums;
using Domain.Models;
using Domain.Models.Equipments;
using Shared.Dtos;

namespace Application.UseCases.Tests.Builder
{
    public class TestBuilder: ITestBuilder
    {
        AddSampleUseCase _addSampleUseCase;
        AddAttachmentUseCase _addAttachmentUseCase;
        AddSpecificationUseCase _addSpecificationUseCase;
        AddChangeStatusTestUseCase _addChangeStatusTestUseCase;
        GetEmployeeByEmployeeNumberUseCase  _getEmployeeByEmployeeNumberUseCase;
        public TestBuilder(
            AddSampleUseCase addSampleUseCase,
            AddAttachmentUseCase addAttachmentUseCase,
            AddSpecificationUseCase addSpecificationUseCase,
            AddChangeStatusTestUseCase addChangeStatusTestUseCase,
            GetEmployeeByEmployeeNumberUseCase  getEmployeeByEmployeeNumberUseCase
            )
        {
            _addSampleUseCase = addSampleUseCase;
            _addAttachmentUseCase = addAttachmentUseCase;
            _addSpecificationUseCase = addSpecificationUseCase;
            _addChangeStatusTestUseCase = addChangeStatusTestUseCase; 
            _getEmployeeByEmployeeNumberUseCase = getEmployeeByEmployeeNumberUseCase;

                  
        }
         private int _id;
        private string _name;
        private string _description;
        private DateTime _start;
        private DateTime _end;
        private SampleDto _samples;
        private string _specialInstructions;
        private AttachmentDto _profile;
        private List<AttachmentDto> _attachments = new List<AttachmentDto>();
        private EmployeeDto _engineer;
        private List<EmployeeDto> _technicians = new List<EmployeeDto>();
        private List<SpecificationDto> _specifications = new List<SpecificationDto>();
        private List<Equipment> _equipments = new List<Equipment>();
        private TestStatusEnum _status;
        private List<ChangeStatusTest> _changeStatusTests = new List<ChangeStatusTest>();
        private string _lastUpdatedMessage;
        private int? _idRequest;
        private List<GenericUpdateDto> _updates = new List<GenericUpdateDto>();

        public  async Task<ITestBuilder> SetId(int id)
        {
            _id = id;
            return await Task.FromResult(this);
        }

        public  async Task<ITestBuilder> SetName(string name)
        {
            _name = name;
            return await Task.FromResult(this);
        }

        public  async Task<ITestBuilder> SetDescription(string description)
        {
            _description = description;
            return await Task.FromResult(this);
        }

        public async Task<ITestBuilder> SetStart(DateTime start)
        {
            _start = start;
            return await Task.FromResult(this);
        }

        public async Task<ITestBuilder> SetEnd(DateTime end)
        {
            _end = end;
            return await Task.FromResult(this);
        }

        public async Task<ITestBuilder> SetSamples(SampleDto samples)
        {
            var result = await _addSampleUseCase.Execute(samples);
            samples.Id =  result.Id??0;
            _samples = samples;
            return await Task.FromResult(this);
        }

        public async Task<ITestBuilder> SetSpecialInstructions(string instructions)
        {
            _specialInstructions = instructions;
            return await Task.FromResult(this);
        }

        public async  Task<ITestBuilder> SetProfile(AttachmentDto profile)
        {
            if(profile.Name == "" || profile == null){
                return await Task.FromResult(this);
            }
            var result =await _addAttachmentUseCase.Execute(profile);
            profile.Id =  result.Id??0;
            _profile = profile;
            return await Task.FromResult(this);
        }

        public async Task<ITestBuilder> SetAttachments(List<AttachmentDto> attachments)
        {
            foreach(var attachment in attachments){
                var result = await _addAttachmentUseCase.Execute(attachment);
                attachment.Id =  result.Id??0;                           
            }
            _attachments = attachments;
            return await Task.FromResult(this);
        }

        public  async Task<ITestBuilder> SetEngineer(EmployeeDto engineer)
        {

            if(engineer.EmployeeNumber == "" || engineer.EmployeeNumber == "0" || engineer == null){
                return await Task.FromResult(this);}

             var result =  await _getEmployeeByEmployeeNumberUseCase.Execute(engineer.EmployeeNumber);
                engineer.Id  = result.Id;
            
            _engineer = engineer;
            return await Task.FromResult(this);
        }

        public async Task<ITestBuilder> SetTechnicians(List<EmployeeDto> technicians)
        {  
            /*foreach(var tech in technicians){
                var result = await _addEmployeeUseCase.Execute(tech);
                tech.Id = result.Id??0;                           
            }*/
            _technicians = technicians;
            return await Task.FromResult(this);           
        }

        public async Task<ITestBuilder> SetSpecifications(List<SpecificationDto> specifications)
        {
            foreach(SpecificationDto specification in specifications){
                var result = await _addSpecificationUseCase.Execute(specification);
                specification.Id = result.Id ?? 0;                
            }
            _specifications = specifications;
            return await Task.FromResult(this);
        }

        public async Task<ITestBuilder> SetEquipments(List<Equipment> equipments)
        {
            _equipments = equipments;
            return await Task.FromResult(this);
        }

        public async Task<ITestBuilder> SetStatus(TestStatusEnum status)
        {
            _status = status;
            return await Task.FromResult(this);
        }

        public async Task<ITestBuilder> SetChangeStatusTests(List<ChangeStatusTest> changeStatusTests)
        {
            foreach (ChangeStatusTest changeStatusTest in changeStatusTests)
            {
                if(changeStatusTest.Id<=0 || changeStatusTest == null){
                    var result =await _addChangeStatusTestUseCase.Execute(changeStatusTest);
                    changeStatusTest.Id =  result.Id??0;
                }else{
                    
                }
            }
            _changeStatusTests = changeStatusTests;
            return await Task.FromResult(this);
        }

        public async Task<ITestBuilder> SetLastUpdatedMessage(string message)
        {
            _lastUpdatedMessage = message;
            return await Task.FromResult(this);
        }

               public async Task<ITestBuilder> SetIdRequest(int? idRequest)
        {
            _idRequest = idRequest;
            return await Task.FromResult(this);
        }

        public async Task<ITestBuilder> SetUpdates(List<GenericUpdateDto> updates)
        {
            _updates = updates;
            return await Task.FromResult(this);
        }

        public async Task<TestDto> Build()
        {
            return await Task.FromResult(new TestDto
            {
                Id = _id,
                Name = _name,
                Description = _description,
                Start = _start,
                End = _end,
                Samples = _samples,
                SpecialInstructions = _specialInstructions,
                Profile = _profile,
                Attachments = _attachments,
                Enginner = _engineer,
                Technicians = _technicians,
                Specifications = _specifications,
                Equipments = _equipments,
                Status = _status,
                changeStatusTest = _changeStatusTests,
                LastUpdatedMessage = _lastUpdatedMessage,
                idRequest = _idRequest,
                updates = _updates
            });
        }
    }
}
