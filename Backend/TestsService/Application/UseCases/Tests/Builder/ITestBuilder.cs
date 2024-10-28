using Domain.Enums;
using Domain.Models;
using Domain.Models.Equipments;
using Shared.Dtos;

namespace Application.UseCases.Tests.Builder
{
    public interface ITestBuilder
    {
        Task<ITestBuilder> SetId(int id);
        Task<ITestBuilder> SetName(string name);
        Task<ITestBuilder> SetDescription(string description);
        Task<ITestBuilder> SetStart(DateTime start);
        Task<ITestBuilder> SetEnd(DateTime end);
        Task<ITestBuilder> SetSamples(SampleDto samples);
        Task<ITestBuilder> SetSpecialInstructions(string instructions);
        Task<ITestBuilder> SetProfile(AttachmentDto profile);
        Task<ITestBuilder> SetAttachments(List<AttachmentDto> attachments);
        Task<ITestBuilder> SetEngineer(EmployeeDto engineer);
        Task<ITestBuilder> SetTechnicians(List<EmployeeDto> technicians);
        Task<ITestBuilder> SetSpecifications(List<SpecificationDto> specifications);
        Task<ITestBuilder> SetEquipments(List<Equipment> equipments);
        Task<ITestBuilder> SetStatus(TestStatusEnum status);
        Task<ITestBuilder> SetChangeStatusTests(List<ChangeStatusTest> changeStatusTests);
        Task<ITestBuilder> SetLastUpdatedMessage(string message);
        Task<ITestBuilder> SetIdRequest(int? idRequest);
        Task<ITestBuilder> SetUpdates(List<GenericUpdateDto> updates);
        Task<TestDto> Build();
    }
}