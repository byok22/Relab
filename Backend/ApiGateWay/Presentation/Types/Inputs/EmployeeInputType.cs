using GraphQL.Types;
using Domain.Models.Employees;

namespace Presentation.Types
{
    public class EmployeeInputType : InputObjectGraphType<Employee>
    {
        public EmployeeInputType()
        {
            Name = "EmployeeInput";
            //id
            Field<IntGraphType>("id").Description("Employee identifier");
            Field<StringGraphType>("employeeNumber").Description("Employee number identifier");
            Field<StringGraphType>("name").Description("Name of the employee");
            Field<EmployeeEnumType>("employeeType").Description("Type of the employee");
        }
    }
}
