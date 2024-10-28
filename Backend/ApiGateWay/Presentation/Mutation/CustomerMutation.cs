

using Domain.Services;
using GraphQL;
using GraphQL.Types;
using Presentation.Types;
using Presentation.Types.Customers.Input;
using Shared.Dtos;

namespace Presentation.Mutation
{
    public class CustomerMutation: ObjectGraphType
    {      
        public CustomerMutation(ICustomerService customerService)
        {
            Field<GenericResponseType>("addCustomer")
                .Description("Add Customer")
                .Arguments(new QueryArguments(
                    new QueryArgument<CustomerInputType> {Name = "customer"}
                ))
                .Resolve(context =>
                {
                    var customer = context.GetArgument<CustomerDto>("customer");
                    return customerService.AddCustomer(customer).Result;
                });

            Field<GenericResponseType>("updateCustomer")
                .Description("Update Customer")
                .Arguments(new QueryArguments(
                    new QueryArgument<CustomerInputType> {Name = "customer"}
                ))
                .Resolve(context =>
                {
                    var customer = context.GetArgument<CustomerDto>("customer");
                    return customerService.PatchCustomer(customer).Result;
                });

           /* Field<StringGraphType>("deleteCustomer")
                .Description("Delete Customer")
                .Arguments(new QueryArguments(
                    new QueryArgument<IntGraphType> {Name = "customerId"}
                ))
                .Resolve(context =>
                {
                    var customerId = context.GetArgument<int>("customerId");
                    Task.WhenAll(customerService.RemoveCustomer(customerId));
                    return "The Customer against this Id" + customerId.ToString() + "has been deleted";
                });*/
        }

        
    }
}