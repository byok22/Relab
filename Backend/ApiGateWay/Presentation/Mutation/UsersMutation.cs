using Domain.Services;
using GraphQL;
using GraphQL.Types;
using Presentation.Types;
using Shared.Dtos;

namespace Presentation.Mutation
{
    public class UsersMutation: ObjectGraphType
    {
        public UsersMutation(IUsersMicroService _service)
        {

             Field<UserType>("addUser")
                .Description("Add User") 
                .Arguments(new QueryArguments(
                    new QueryArgument<UserInputType>{Name = "user"}
                ) )              
                .Resolve(context =>
                { 
                    var user = context.GetArgument<UserDto>("user");
                    return  _service.AddUser(user).Result;
                });

                Field<UserType>("updateUser")
                .Description("Update User") 
                .Arguments(new QueryArguments(                   
                    new QueryArgument<UserInputType>{Name = "user"}
                ) )              
                .Resolve(context =>
                {                  
                    var user = context.GetArgument<UserDto>("user");
                    return  _service.UpdateUser(
                    user                     
                    ).Result;
                });
            
        }
        
    }
}