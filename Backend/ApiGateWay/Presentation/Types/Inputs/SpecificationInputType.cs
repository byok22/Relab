using GraphQL.Types;
using Domain.Models;

namespace Presentation.Types
{
    public class SpecificationInputType : InputObjectGraphType<Specification>
    {
        public SpecificationInputType()
        {
            Name = "SpecificationInput";
            Field<StringGraphType>("specificationName").Description("Name of the specification");
            Field<StringGraphType>("details").Description("Details of the specification");
        }
    }
}