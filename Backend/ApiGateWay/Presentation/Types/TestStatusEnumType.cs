using GraphQL.Types;
using Domain.Enums;

namespace Presentation.Types
{
    public class TestStatusEnumType : EnumerationGraphType<TestStatusEnum>
    {
        public TestStatusEnumType()
        {
            Name = "TestStatusEnum";
            Description = "Enumeration for the status of a test";
        }
    }
}
