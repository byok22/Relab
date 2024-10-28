using GraphQL.Types;
using Domain.Models;
using GraphQL.Upload.AspNetCore;

namespace Presentation.Types
{
    public class AttachmentType : ObjectGraphType<Attachment>
    {
        public AttachmentType()
        {
            
            Field(x => x.Name).Description("Name of the attachment");
            Field(x => x.Url).Description("URL of the attachment");
            Field(x => x.Location).Description("Location of the attachment");
            Field(x => x.Id).Description("Id of the attachment");
            

            // Para el campo `File`, si deseas incluirlo en el esquema GraphQL, necesitarías manejarlo de una manera adecuada, 
            // por ejemplo, convirtiéndolo a Base64 o tratándolo como un tipo de archivo separado.
/*
             Field<StringGraphType>("file")
                .Description("Base64 encoded file")
                .Resolve(context => Convert.ToBase64String(context.Source.File));*/
          /*  Field<UploadGraphType>("file")
                .Description("File attachment")
                .Resolve(context => context.Source.File);

                */
            
        }
    }
}
