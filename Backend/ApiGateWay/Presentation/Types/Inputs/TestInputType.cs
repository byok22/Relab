using ApiGateWay.Presentation.Types.Inputs;
using GraphQL.Types;
using Shared.Dtos;

namespace Presentation.Types
{
    public class TestInputType : InputObjectGraphType<TestDto>
    {
        public TestInputType()
        {
            Name = "TestInput";
            Description = "Input type for Test data";
            Field<IntGraphType>("id")
                .Description("Id of the Test");

            Field<StringGraphType>("name")
                .Description("Name of the Test");

            Field<StringGraphType>("description")
                .Description("Description of the Test");

            Field<DateTimeGraphType>("start")
               .Description("Start date of the Test");

            Field<DateTimeGraphType>("end")
                .Description("End date of the Test");

            Field<StringGraphType>("specialInstructions")
                .Description("Special instructions for the Test");

            Field<AttachmentInputType>("profile")
                .Description("Profile attachment for the Test");

            Field<ListGraphType<AttachmentInputType>>("attachments")
                .Description("List of attachments for the Test");

            Field<EmployeeInputType>("enginner")
                .Description("Engineer assigned to the Test");

            Field<ListGraphType<EmployeeInputType>>("technicians")
                .Description("List of technicians involved in the Test");

            Field<ListGraphType<SpecificationInputType>>("specifications")
                .Description("List of specifications for the Test");

            Field<SamplesInputType>("samples")
                .Description("List of specifications for the Test");

            Field<ListGraphType<EquipmentInputType>>("equipments")
                .Description("List of equipments used in the Test");

            Field<TestStatusEnumType>("status")
                .Description("Current status of the Test");

            Field<ListGraphType<ChangeStatusTestInputType>>("changeStatusTest")
                .Description("List of status changes for the Test");

            Field<StringGraphType>("lastUpdatedMessage")
                .Description("Message for the last update made to the Test");
            
            Field<IntGraphType>("idRequest")
                .Description("idRequest to the Test");

            Field<ListGraphType<GenericUpdateInputType>>("updates").Description("Updates of the Test");           


        }
    }
}
