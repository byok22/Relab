using GraphQL.Types;
using GraphQL.Upload.AspNetCore;

namespace ApiGateWay.Presentation.Types.Inputs
{
    public class FileUploadInputType : InputObjectGraphType
{
    public FileUploadInputType()
    {
        Name = "FileUpload";
        Field<NonNullGraphType<StringGraphType>>("fileName");
        Field<NonNullGraphType<StringGraphType>>("contentType");
        Field<NonNullGraphType<UploadGraphType>>("file");
    }
}
}