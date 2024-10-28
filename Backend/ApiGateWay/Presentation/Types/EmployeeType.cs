using GraphQL.Types;
using Domain.Models.Employees;

namespace Presentation.Types
{
    public class EmployeeType : ObjectGraphType<Employee>
    {
        public EmployeeType()
        {
            //id
            Field(x => x.Id).Description("Employee identifier");
            Field(x => x.EmployeeNumber).Description("Employee number identifier");
            Field(x => x.Name).Description("Name of the employee");
         //   Field(x=>x.EmployeeType).Description("EmployeeType of the employee");
            Field<EmployeeEnumType>("employeeType").Description("Type of the employee");
        }
    }

   public class EmployeeEnumType : EnumerationGraphType<EmployeeTypeEnum>
    {
        public EmployeeEnumType()
        {
             Name = "EmployeeEnumType";
        }
    
    }
}
