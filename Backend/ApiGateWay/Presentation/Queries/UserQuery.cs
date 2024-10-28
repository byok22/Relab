using Domain.Services;
using GraphQL;
using GraphQL.Types;
using Presentation.Types;

namespace Presentation.Queries
{
    public class UserQuery: ObjectGraphType
    {
        public UserQuery(
            IUsersMicroService usersMicroService
           // GetUserByIDUseCase getUserByIDUseCase,
         //   GetUsersByTypeUseCase getUsersByTypeUseCase,
            //GetUserByEmployeeAccountUseCase getUserByEmployeeAccountUseCase,
            //GetUserInfoLdapByEmployeeAccountUseCase getUserInfoLdapByEmployeeAccountUseCase
            )
        {

             Field<ListGraphType<UserType>>("users").Description("Retrieve Profiles")           
            .Resolve(context=>{
                return usersMicroService.GetAllUsers().Result;
            })
            ;

           Field<UserType>("user").Description("Retrieve User By ID")
            .Arguments(new QueryArguments(
                new QueryArgument<IntGraphType>{Name= "userId"}
            ))
            .Resolve(context=>{
                return usersMicroService.GetUserByID(context.GetArgument<int>("userId")).Result;
            }) ;

            Field<UserType>("userByEmployeeAccount").Description("Retrieve User By Account")
            .Arguments(new QueryArguments(
                new QueryArgument<StringGraphType>{Name= "employeeAccount"}
            ))
            .Resolve(context=>{
                return usersMicroService.GetUserByEmployeeAccount(context.GetArgument<string>("employeeAccount")).Result;
            });

            Field<UserType>("userInfoLDapByEmployeeAccount").Description("Retrieve User From LDA by EmployeeAccount")
            .Arguments(new QueryArguments(
                new QueryArgument<StringGraphType>{Name= "employeeAccount"}
            ))
            .Resolve(context=>{
                return usersMicroService.GetUserInfoLdapByEmployeeAccount(context.GetArgument<string>("employeeAccount")).Result;
            });

             Field<ListGraphType<UserType>>("UsersByType").Description("Retrieve Users By Type")
            .Arguments(new QueryArguments(
                new QueryArgument<StringGraphType>{Name= "userType"}
            ))
            .Resolve(context=>{
                return usersMicroService.GetUsersByType(context.GetArgument<string>("userType")).Result;
            })
            ;
        }
        
    }
}