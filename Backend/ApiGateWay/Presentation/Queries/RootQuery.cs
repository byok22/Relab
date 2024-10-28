using GraphQL.Types;

namespace Presentation.Queries
{
    public class RootQuery : ObjectGraphType
    {
        public RootQuery()
        {
            Field<UserQuery>("userQuery").Resolve(context=> new {});
                Field<UserProfileQuery>("profileQuery").Resolve(context=> new {});
                 Field<TestQuery>("testQuery").Resolve(context=> new {});
                 Field<TestRequestQuery>("testRequestQuery").Resolve(context=> new {});
                 Field<EquipmentQuery>("equipmentQuery").Resolve(context=> new {});
                    Field<EmployeeQuery>("employeeQuery").Resolve(context=> new {});
                    Field<CustomerQuery>("customerQuery").Resolve(context=> new {});
              //  Field<UserQuery>("userQuery").Description("Query for user-related operations");
             /* Field<UserProfileQuery>("profileQuery").Description("Query for user profile-related operations");
              Field<TestQuery>("testQuery").Description("Query for test-related operations");
            Field<TestRequestQuery>("testRequestQuery").Description("Query for test-Reques operations");*/

              

              /*
            // Agrega la consulta para los usuarios
            Field<UserQuery>("userQuery")
                .Description("Query for user-related operations");

            // Agrega la consulta para los perfiles de usuario
            Field<UserProfileQuery>("profileQuery")
                .Description("Query for user profile-related operations");
            
            // Agrega la consulta para los tests
            Field<TestQuery>("testQuery")
                .Description("Query for test-related operations");*/

           
        }
    }
}
