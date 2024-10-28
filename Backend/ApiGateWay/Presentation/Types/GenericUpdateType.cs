using GraphQL.Types;
using Domain.Models.Generics;

namespace Presentation.Types
{
    public class GenericUpdateType : ObjectGraphType<GenericUpdate>
    {
        public GenericUpdateType()
        {
            Field(x => x.Id).Description("ID of the generic update");
            Field(x => x.UpdatedAt).Description("Date and time of the update");
            Field(x => x.User, nullable: true).Description("User who made the update");
            Field(x => x.Message, nullable: true).Description("Message related to the update");
            Field(x => x.Changes, nullable: true).Description("Changes made in the update");
        }
    }
}
