using GraphQL.Types;
using Domain.Models.Tests;

namespace Presentation.Types
{
    public class ChangeStatusTestInputType : InputObjectGraphType<ChangeStatusTest>
    {
        public ChangeStatusTestInputType()
        {
            Name = "ChangeStatusTestInput";
            Field<TestStatusEnumType>("status").Description("Status of the Test");
            Field<StringGraphType>("message").Description("Message of Change Status");
            Field<AttachmentInputType>("attachment").Description("Attachment");
            Field<IntGraphType>("idUser").Description("Id User");
            Field<IntGraphType>("idTest").Description("Id Test");
        }
    }
}
