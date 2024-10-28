using GraphQL.Types;
using Shared.Dtos;

namespace Presentation.Types
{
    public class TestRequestType : ObjectGraphType<TestRequestDto>
    {
        public TestRequestType()
        {
            Field(x => x.Id).Description("ID of the test request");
            Field(x => x.Status, type: typeof(TestRequestsStatusEnumType)).Description("Status of the test request");
            Field(x => x.Description).Description("Description of the test request");
            Field(x => x.Start).Description("Start date of the test request");
            Field(x => x.End).Description("End date of the test request");

            // Elimina este campo porque ya no existe en el DTO
            // Field(x => x.Profile, nullable: true).Description("Profile attachment for the test request");
            Field<ListGraphType<TestType>>("tests").Description("Tests related to the test request");
            Field(x => x.TestsCount, nullable: true).Description("Number of tests related to the test request");
            Field(x => x.Active).Description("Whether the test request is active");
            Field(x => x.CreatedBy, nullable: true).Description("User who created the test request");
            Field(x => x.CreatedAt).Description("Creation date of the test request");
            Field<ListGraphType<GenericUpdateType>>("updates").Description("Updates related to the test request");
            Field<ListGraphType<ChangeStatusTestRequestType>>("changes").Description("Changes related to the status of the test request");
        }
    }
}
