using GraphQL.Types;
using Shared.Dtos;

namespace Presentation.Types
{
    public class TestRequestInputType : InputObjectGraphType<TestRequestDto>
    {
        public TestRequestInputType()
        {
            Name = "TestRequestInput";
            Field<IntGraphType>("id").Description("Id of the Test Request");
            Field<TestRequestsStatusEnumType>("status").Description("Status of the Test Request");
            Field<StringGraphType>("description").Description("Description of the Test Request");
            Field<DateTimeGraphType>("start").Description("Start date of the Test Request");
            Field<DateTimeGraphType>("end").Description("End date of the Test Request");
            // Elimina este campo porque se ha eliminado del DTO
            // Field<AttachmentInputType>("profile").Description("Profile of TestRequest");
            Field<ListGraphType<TestInputType>>("tests").Description("Tests of the Test Request");            
            Field<BooleanGraphType>("active").Description("Is the Test Request active");
            Field<UserInputType>("createdby").Description("Created By");
            Field<DateTimeGraphType>("createdAt").Description("Creation date of the Test Request");
            Field<ListGraphType<GenericUpdateInputType>>("updates").Description("History of updates");
            Field<ListGraphType<ChangeStatusTestInputType>>("changes").Description("Changes of Status");
        }
    }
}
