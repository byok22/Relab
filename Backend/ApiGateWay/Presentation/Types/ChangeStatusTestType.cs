using Domain.Models.Tests;
using GraphQL.Types;
using Presentation.Types;

namespace Presentation.Types
{
    public class ChangeStatusTestType: ObjectGraphType<ChangeStatusTest>
    {
        public ChangeStatusTestType()
        {
            Field<TestStatusEnumType>("status").Description("Status of the Test");
            Field(x => x.Message).Description("Message of Change Status");
            Field<AttachmentType>("attachment").Description("Attachment");
            Field(x => x.idUser).Description("Id User");   
            Field(x => x.idTest).Description("Id Test");          
        }        
    }
}