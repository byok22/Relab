using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Services;
using GraphQL.Types;
using Presentation.Types;

namespace Presentation.Queries
{
    public class UserProfileQuery: ObjectGraphType
    {
        public UserProfileQuery(  IUsersMicroService usersMicroService)
        {
            Field<ListGraphType<UserProfileType>>("Profiles").Description("Retrieve Profiles")           
            .Resolve(context=>{
                return usersMicroService.GetUserProfiles().Result;
            })
            ;
            
        }
        
    }
}