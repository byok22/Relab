using GraphQL.Types;

namespace Presentation.Mutation
{
    public class RootMutation: ObjectGraphType
    {
        public RootMutation()
        {
            Field<UsersMutation>("userMutation").Description("Mutations For User").Resolve(context=> new {});
            Field<TestMutation>("testMutation").Description("Mutations For Test").Resolve(context=> new {});
            Field<TestRequestMutation>("testRequestMutation").Description("Mutations For TestRequest").Resolve(context=> new {});
            Field<EquipmentMutation>("equipmentMutation").Description("Mutations For Equipment").Resolve(context=> new {});
            Field<EmployeeMutation>("employeeMutation").Description("Mutations For Employee").Resolve(context=> new {});
            Field<CustomerMutation>("customerMutation").Description("Mutations For Customer").Resolve(context=> new {});
             ///ADD OTHERS
          
        }
        
    }
}