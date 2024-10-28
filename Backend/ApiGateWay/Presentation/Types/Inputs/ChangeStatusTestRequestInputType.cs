
using Domain.Models.TestRequests;
using GraphQL.Types;

namespace Presentation.Types.Inputs
{
    public class ChangeStatusTestRequestInputType: InputObjectGraphType<ChangeStatusTestRequest>
    {
        public ChangeStatusTestRequestInputType()
        {
            Name = "ChangeStatusTestRequestInput";
            Field<TestRequestsStatusEnumType>("status").Description("Status of the Test Request");
            Field<StringGraphType>("message").Description("Message of Change Status");
            Field<AttachmentInputType>("attachment").Description("Attachment");
            Field<IntGraphType>("idUser").Description("Id User");           
        }
    }
}