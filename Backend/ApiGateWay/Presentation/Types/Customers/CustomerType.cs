
using GraphQL.Types;
using Shared.Dtos;

namespace Presentation.Types.Customers
{
    public class CustomerType: ObjectGraphType<CustomerDto>
    {
        public CustomerType()
        {
            Field(x => x.Id).Description("The unique identifier of the customer.");
            Field(x => x.CustomerID).Description("The customer UUID of the customer.");
            Field(x => x.CustomerName).Description("The name of the customer.");
            Field(x => x.Division).Description("The division the customer belongs to.");
            Field(x => x.BuildingID).Description("The ID of the building associated with the customer.");
            Field(x => x.Building).Description("The building associated with the customer.");
            Field(x => x.Available).Description("Indicates whether the customer is available.");
        }
        
    }
}