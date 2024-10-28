using GraphQL.Types;
using Domain.Models;

namespace Presentation.Types
{
    public class SpecificationType : ObjectGraphType<Specification>
    {
        public SpecificationType()
        {
            Field(x => x.SpecificationName, nullable: true).Description("Name of the specification");
            Field(x => x.Details).Description("Details of the specification");
        }
    }
}
