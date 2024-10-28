using GraphQL.Types;
using Shared.Dtos;

namespace Presentation.Types.Customers.Input
{
    public class CustomerInputType : InputObjectGraphType<CustomerDto>
    {
        public CustomerInputType()
        {
            Name = "CustomerInput";
            Field<IntGraphType>("id").Description("Id of the customer");
            Field<StringGraphType>("customerID").Description("UUID of the customer");
            Field<StringGraphType>("customerName").Description("Name of the customer");
            Field<StringGraphType>("division").Description("Division of the customer");
            Field<IntGraphType>("buildingID").Description("Building ID of the customer");
            Field<StringGraphType>("building").Description("Building of the customer");
            Field<BooleanGraphType>("available").Description("Availability of the customer");
        }
    }
}
