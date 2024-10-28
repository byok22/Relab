using GraphQL.Types;
using Domain.Models;

namespace Presentation.Types
{
    public class AttachmentInputType : InputObjectGraphType<Attachment>
    {
        public AttachmentInputType()
        {
            Name = "AttachmentInput";
            Field<IntGraphType>("id").Description("Id of the attachment");
            Field<StringGraphType>("name").Description("Name of the attachment");
            Field<StringGraphType>("url").Description("URL of the attachment");
            Field<StringGraphType>("location").Description("Location of the attachment");
            //Field<UploadGraphType>("file").Description("File");
        }
    }
}



