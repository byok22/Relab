using Domain.Enums;
using Domain.Services;
using GraphQL;
using GraphQL.Types;
using Presentation.Types;

namespace Presentation.Queries
{
    public class TestRequestQuery : ObjectGraphType
    {
        public TestRequestQuery(
            ITestRequestService service
        )
        {
          
            // Campo para obtener todos los test por estado de solicitud
            Field<ListGraphType<TestRequestType>>("testRequestsByStatus")
                .Description("Retrieve test requests by status")
                .Arguments(new QueryArguments(
                    new QueryArgument<TestRequestsStatusEnumType> { Name = "status" }
                ))
                .ResolveAsync(async context =>
                {
                    var status = context.GetArgument<TestRequestsStatus>("status");
                    return await service.GetAllTestRequestsByStatus(status);
                });

            // Campo para obtener todas las solicitudes de test
            Field<ListGraphType<TestRequestType>>("testRequests")
                .Description("Retrieve all test requests")
                .ResolveAsync(async context => await service.GetAllTestRequests());


                 // Campo para obtener un test Request por ID
            Field<TestRequestType>("GetTestRequestsById")
                .Description("Retrieve test by ID")
                .Arguments(new QueryArguments(
                    new QueryArgument<IntGraphType> { Name = "id" }
                ))
                .ResolveAsync(async context =>
                {
                    var id = context.GetArgument<int>("id");
                    var tests = await service.GetTestRequestsById(id);
                    return tests;
                });
        }
    }
}
