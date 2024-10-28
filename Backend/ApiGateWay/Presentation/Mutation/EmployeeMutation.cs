using Domain.Models.Employees;
using Domain.Services;
using GraphQL;
using GraphQL.Types;
using Presentation.Services;
using Presentation.Types;

namespace Presentation.Mutation
{
    public class EmployeeMutation: ObjectGraphType
    {
        public EmployeeMutation( IEmployeesService employeesService)
        {
              Field<GenericResponseType>("addEmployee")
                .Description("Add Employee") 
                .Arguments(new QueryArguments(
                    new QueryArgument<EmployeeInputType>{Name = "employee"}
                ) )              
                .Resolve(context =>
                { 
                    var employee = context.GetArgument<Employee>("employee");
                    return  employeesService.AddEmployee(employee).Result;
                });

                Field<GenericResponseType>("updateEmployee")
                .Description("Update Employee") 
                .Arguments(new QueryArguments(                   
                    new QueryArgument<EmployeeInputType>{Name = "employee"}
                ) )              
                .Resolve(context =>
                {                  
                    var employee = context.GetArgument<Employee>("employee");
                    return   employeesService.UpdateEmployee(employee).Result;
                });

                 Field<StringGraphType>("deleteEmployee")
                .Description("Delete Employee") 
                .Arguments(new QueryArguments(
                     new QueryArgument<IntGraphType>{Name = "employeeId"}                   
                ) )              
                .Resolve(context =>
                {
                    var employeeId = context.GetArgument<int>("employeeId");
                    Task.WhenAll(employeesService.RemoveEmployee(employeeId));
                    return "The Employee against this Id"+ employeeId.ToString() + "has been deleted";                    
                });
        }
        
    }
}