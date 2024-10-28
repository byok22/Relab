using Domain.Enums;
using Domain.Services;
using GraphQL;
using GraphQL.Types;
using Presentation.Types;

namespace Presentation.Queries
{
    public class TestQuery : ObjectGraphType
    {
        public TestQuery(
            ITestsMicroServices testsMicroService
        )
        {
            Description="Managment Test";
            // Campo para obtener todos los test
            Field<ListGraphType<TestType>>("GetAllTest")
                .Description("Retrieve all tests")
                .Resolve(context => {
                   
                    return testsMicroService.GetAllTest().Result;                  
                });

             // Campo para obtener todos los test por estado de solicitud
            Field<ListGraphType<TestType>>("GetAllTestByStatus")
                .Description("Retrieve test  by status")
                .Arguments(new QueryArguments(
                    new QueryArgument<TestStatusEnumType> { Name = "status" }
                ))
                .ResolveAsync(async context =>
                {
                    var status = context.GetArgument<TestStatusEnum>("status");
                    return await testsMicroService.GetAllTestByStatus(status);
                });

            // Campo para obtener un test por ID
            Field<TestType>("GetTestById")
                .Description("Retrieve test by ID")
                .Arguments(new QueryArguments(
                    new QueryArgument<IntGraphType> { Name = "id" }
                ))
                .ResolveAsync(async context =>
                {
                    var testId = context.GetArgument<int>("id");
                    var tests = await testsMicroService.GetTestById(testId);
                    return tests;
                });
           
        }
    }
}
