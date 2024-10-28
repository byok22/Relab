using GraphQL.Types;
using Shared.Dtos;

namespace Presentation.Types
{
    public class TestType : ObjectGraphType<TestDto>
    {
        public TestType()
        {
            Description="Test Type Transform ";
            Field(x => x.Id).Description("Id For Test");
            Field(x => x.Name).Description("Name of Test");
            Field(x => x.Description).Description("Description of the Test");
            Field(x => x.Start).Description("Start Date of the Test");
            Field(x => x.End).Description("End Date of the Test");
            Field(x => x.Samples, nullable: true).Description("Samples information");
            Field(x => x.SpecialInstructions, nullable: true).Description("Special instructions for the Test");                      
            Field<AttachmentType>("profile").Description("Profile Attachment");
            Field<ListGraphType<AttachmentType>>("attachments").Description("List of Attachments");         
            Field(x => x.Enginner, nullable: true).Description("Engineer assigned to the Test");
            Field<ListGraphType<EmployeeType>>("technicians").Description("List of Technicians");
            Field<ListGraphType<SpecificationType>>("specifications").Description("List of Specifications");
            Field<ListGraphType<EquipmentType>>("equipments").Description("List of Equipments");
            Field<TestStatusEnumType>("status").Description("Status of the Test");
            Field<ListGraphType<ChangeStatusTestType>>("changeStatusTest").Description("List of Change Status");             
            Field(x => x.LastUpdatedMessage).Description("Last Updated Message");
            Field(x => x.idRequest, nullable: true).Description("idRequest");
            Field<ListGraphType<GenericUpdateType>>("updates").Description("Updates of the Test"); 
            Field(x => x.AttachmentsCount).Description("Count of Attachments");
            Field(x => x.TechniciansCount).Description("Count of Technicians");
            Field(x => x.SpecificationsCount).Description("Count of Specifications");
            Field(x => x.EquipmentsCount).Description("Count of Equipments");           
        }
    }
}
