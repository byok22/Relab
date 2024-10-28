using Shared.Dtos;
using GraphQL.Types;

namespace Presentation.Types
{
    public class UserType: ObjectGraphType<UserDto>
    {
        public UserType()
        {
            Field(x => x.Id).Description("Id of the User");
            Field(x => x.UserName).Description("Name of User");
            Field(x => x.Email).Description("Email of the User");
            Field(x=> x.EmployeeAccount).Description("EmployeeAccount of the User");            
        }
        
    }
}