using Domain.Enums;
using Domain.Models.TestRequests;
using Domain.Services;
using GraphQL;
using GraphQL.Types;
using Presentation.Types;
using Presentation.Types.Inputs;
using Shared.Dtos;

namespace Presentation.Mutation
{
    public class TestRequestMutation: ObjectGraphType
    {
        public TestRequestMutation(ITestRequestService service)
        {

            //Mutation For update Status
            Field<GenericResponseType>("ChangeStatusTestRequest")
            .Description("Change Status of Test Request")
            .Arguments(new QueryArguments(
                    new QueryArgument<IntGraphType> { Name = "id" },
                    new QueryArgument<ChangeStatusTestRequestInputType> { Name = "status" }            
                ))
            .Resolve(context =>
                {
                    var id = context.GetArgument<int>("id");
                    var status = context.GetArgument<ChangeStatusTestRequest>("status");                   
                  
                    return service.ChangeStatusTestRequest(id, status).Result; // Modify as needed
                });


             //Mutation For update Status
            Field<GenericResponseType>("ApproveOrRejectTestRequest")
            .Description("Change Status of Test Request")
            .Arguments(new QueryArguments(
                    new QueryArgument<IntGraphType> { Name = "id" },
                    new QueryArgument<ChangeStatusTestRequestInputType> { Name = "status" }            
                ))
            .Resolve(context =>
                {
                    var id = context.GetArgument<int>("id");
                    var status = context.GetArgument<ChangeStatusTestRequest>("status");                   
                  
                    return service.ApproveOrRejectTestRequest(id, status).Result; // Modify as needed
                });



                
            // Mutation for updating the status of a TestRequest
            Field<GenericResponseType>("UpdateTestRequestStatus")
                .Description("Update Test Request Status")
                .Arguments(new QueryArguments(
                    new QueryArgument<IntGraphType> { Name = "id" },
                    new QueryArgument<TestRequestsStatusEnumType> { Name = "status" },
                    new QueryArgument<StringGraphType> { Name = "message" }
                ))
                .Resolve(context =>
                {
                    var id = context.GetArgument<int>("id");
                    var status = context.GetArgument<TestRequestsStatus>("status");
                    var message = context.GetArgument<string>("message");
                    var testRequestDto = new TestRequestDto
                    {
                        Id = id,
                        Status = status,
                        Description = message // Assuming you want to use description for the message
                    };
                    return service.AddTestRequest(testRequestDto).Result; // Modify as needed
                });

          /*Field<GenericResponseType>("ApproveOrRejectTestRequest")
            .Description("Approve Or Reject Test Request")
            .Arguments(new QueryArguments(
                new QueryArgument<IntGraphType> { Name = "id" },
                new QueryArgument<ChangeStatusTestRequestInputType> { Name = "changeStatusTestRequest" }
            ))
            .Resolve(context =>
            {
                try
                {
                    var id = context.GetArgument<int>("id");
                    var changeStatusTestRequest = context.GetArgument<ChangeStatusTestRequest>("changeStatusTestRequest");

                    return service.ApproveOrRejectTestRequest(id, changeStatusTestRequest).Result;
                }
                catch (Exception ex)
                {
                    // Log or handle the exception as necessary
                    return new GenericResponse
                    {
                        IsSuccessful = false,
                        Message = $"An error occurred: {ex.Message}"
                    };
                }
            });*/




                  // Mutation for adding a TestRequest
            Field<GenericResponseType>("AddTestRequest")
                .Description("Add Test Request")
                .Arguments(new QueryArguments(
                    new QueryArgument<TestRequestInputType> { Name = "testRequest" }
                ))
                .Resolve(context =>
                {
                    var testRequestDto = context.GetArgument<TestRequestDto>("testRequest");
                    return service.AddTestRequest(testRequestDto).Result;
                });

            // Mutation for updating a TestRequest
            Field<GenericResponseType>("updateTestRequest")
                .Description("Update Test Request")
                .Arguments(new QueryArguments(
                    new QueryArgument<TestRequestInputType> { Name = "testRequest" }
                ))
                .Resolve(context =>
                {
                    var testRequestDto = context.GetArgument<TestRequestDto>("testRequest");
                    return service.UpdateTestRequest(testRequestDto).Result;
                });

        }
         
    }
}