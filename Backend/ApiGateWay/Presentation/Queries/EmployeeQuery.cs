

using ApiGateWay.Presentation.Types;
using Domain.Models.Employees;
using Domain.Services;
using GraphQL;

using GraphQL.Types;
using Presentation.Services;
using Presentation.Types;

namespace Presentation.Queries
{
    public class EmployeeQuery : ObjectGraphType
    {
        public EmployeeQuery(IEmployeesService employeeService)
        {
            Field<ListGraphType<EmployeeType>>("employees")
                .Description("All Employees")               
                .Resolve(context =>
                {
                   return  employeeService.GetAllEmployees().Result;
                });

                //bytype

            Field<ListGraphType<EmployeeType>>("employeesByType")
                .Description("All Employees by type")
                .Arguments(new QueryArguments(
                    new QueryArgument<EmployeeEnumType>{Name = "employeeType"}
                ) )
                .Resolve(context =>
                {
                    var employeeType = context.GetArgument<EmployeeTypeEnum>("employeeType");
                     return  employeeService.GetEmployeesByType(employeeType).Result;
                });

            Field<EmployeeType>("employee")
                .Description("Retrieve employee by employeeNumber") 
                .Arguments(new QueryArguments(
                    new QueryArgument<StringGraphType>{Name = "employeeNumber"}
                ) )              
                .Resolve(context =>
                {
                   return  employeeService.GetEmployeeByEmployeeNumber(context.GetArgument<string>("employeeNumber")).Result;
                });

        }
    }
}
