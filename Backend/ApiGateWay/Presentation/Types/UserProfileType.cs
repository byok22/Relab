using Domain.Models;
using GraphQL.Types;


namespace Presentation.Types
{
    public class UserProfileType: ObjectGraphType<UserProfile>
    {
        public UserProfileType()
        {
            Field(x => x.Id).Description("Id of the Profile");
            Field(x => x.Name).Description("Name of Profile");
            Field(x => x.Description).Description("Description of the Profile");                   
        }
        
    }
}