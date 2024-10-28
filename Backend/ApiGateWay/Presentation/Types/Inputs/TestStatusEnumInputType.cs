using GraphQL.Types;
using Domain.Enums;

namespace Presentation.Types
{
    public class TestStatusEnumInputType : InputObjectGraphType<TestStatusEnum>
    {
        public TestStatusEnumInputType()
        {
            Name = "TestStatusEnumInput";
            Description = "Enumeration for the status of a test";
        }
    }
}
