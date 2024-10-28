using GraphQL.Types;
using Domain.Models.Generics;

namespace Presentation.Types
{
    public class GenericUpdateInputType : InputObjectGraphType<GenericUpdate>
    {
        public GenericUpdateInputType()
        {
            Name = "GenericUpdateInput";
            Field<IntGraphType>("id").Description("ID of the generic update");
            Field<DateGraphType>("updatedAt").Description("Date and time of the update");
            Field<StringGraphType>("user").Description("User who made the update");
            Field<StringGraphType>("message").Description("Message related to the update");
            Field<StringGraphType>("changes").Description("Changes made in the update");
        }
    }
}
