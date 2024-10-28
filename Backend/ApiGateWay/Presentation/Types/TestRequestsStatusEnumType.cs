using GraphQL.Types;
using Domain.Enums;

namespace Presentation.Types
{
    public class TestRequestsStatusEnumType : EnumerationGraphType<TestRequestsStatus>
    {
        public TestRequestsStatusEnumType()
        {
            Name = "TestRequestsStatusEnum";
            Description = "Enumeration for the status of test requests";            
        }
    }
}
