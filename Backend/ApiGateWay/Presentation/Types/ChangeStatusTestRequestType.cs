using GraphQL.Types;
using Domain.Models.TestRequests;

namespace Presentation.Types
{
    public class ChangeStatusTestRequestType : ObjectGraphType<ChangeStatusTestRequest>
    {
        public ChangeStatusTestRequestType()
        {
            Field(x => x.Status, type: typeof(TestRequestsStatusEnumType)).Description("Status of the test request change");
            Field(x => x.Message).Description("Message related to the status change");
            Field(x => x.Attachment, nullable: true).Description("Attachment related to the status change");
            Field(x => x.IdUser).Description("ID of the user who made the status change");
        }
    }
}
