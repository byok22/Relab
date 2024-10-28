using Domain.Services;
using GraphQL;
using GraphQL.Types;
using Presentation.Types.Customers;

namespace Presentation.Queries
{
    public class CustomerQuery: ObjectGraphType
    {

        public CustomerQuery(ICustomerService customerService){

            /*
              Field<ListGraphType<EmployeeType>>("employees")
                .Description("All Employees")               
                .Resolve(context =>
                {
                   return  employeeService.GetAllEmployees().Result;
                });
            */

            Field<ListGraphType<CustomerType>>("customers")
            .Description("All Customers")
            .Resolve(context => customerService.GetAllCustomer().Result);

            //getbyid
            Field<CustomerType>("customer")
            .Description("Retrieve customer by id")
            .Arguments(new QueryArguments(
                new QueryArgument<IntGraphType>{Name = "id"}
            ))
            .Resolve(context => {
                var id = context.GetArgument<int>("id");
                return customerService.GetCustomerById(id).Result;
            });

            //getbycustomerid
            Field<CustomerType>("customerByCustomerId")
            .Description("Retrieve customer by customer id")
            .Arguments(new QueryArguments(
                new QueryArgument<StringGraphType>{Name = "customerId"}
            ))
            .Resolve(context => {
                var customerId = context.GetArgument<string>("customerId");
                return customerService.GetCustomerByCustomerId(customerId).Result;
            });
          
            
        }

        
    }
}