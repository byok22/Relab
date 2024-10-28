using GraphQL.Types;

namespace Presentation.Types
{
    public class UserInputType: InputObjectGraphType
    {
        
           
       public UserInputType()
       {
        Name="UserInput";
        Field<IntGraphType>("id").Description("Id of the User");
        Field<StringGraphType>("userName").Description("Name of User");
        Field<StringGraphType>("email").Description("Email of User");
        Field<StringGraphType>("employeeAccount").Description("EmployeeeAccount of User");                    
        
       }
    }
}