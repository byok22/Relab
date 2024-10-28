using Domain.Models;
using Domain.Models.Tests;
using Domain.Services;
using GraphQL;
using GraphQL.Types;
using Presentation.Types;
using Shared.Dtos;

namespace Presentation.Mutation
{
    public class TestMutation : ObjectGraphType
    {
        public TestMutation(ITestsMicroServices service)
        {

            // Mutation for adding a Test
             Field<GenericResponseType>(
                "addTest")
                .Description("Add Test")
                .Arguments( new QueryArguments(
                    new QueryArgument<TestInputType> { Name = "test" }
                )).
                Resolve(context =>
                {
                    var testDto = context.GetArgument<TestDto>("test");
                     return service.AddTest(testDto).Result;
                });
            // Mutation for changing the status of a Test
            Field<GenericResponseType>("ChangeStatusTest")
                .Description("Change Status Test")
                .Arguments(new QueryArguments(
                    new QueryArgument<ChangeStatusTestInputType> { Name = "statusChange" }
                ))
                .Resolve(context =>
                {
                    var statusChange = context.GetArgument<ChangeStatusTest>("statusChange");
                    return service.ChangeStatusTest(statusChange).Result;
                });


             


            // Mutation for updating a Test
            Field<GenericResponseType>("UpdateTest")
                .Description("Update Test")
                .Arguments(new QueryArguments(
                    new QueryArgument<TestInputType> { Name = "test" }
                ))
                .Resolve(context =>
                {
                    var testDto = context.GetArgument<TestDto>("test");
                    return service.PatchTest(testDto).Result;
                });    


            //Delete Equipment From Test IdTest IdEquipment 
            Field<GenericResponseType>("DeleteEquipmentFromTest")
                .Description("Delete Equipment From Test")
                .Arguments(new QueryArguments(
                    new QueryArgument<IntGraphType> { Name = "idTest" },
                    new QueryArgument<IntGraphType> { Name = "idEquipment" }
                ))
                .Resolve(context =>
                {
                    var idTest = context.GetArgument<int>("idTest");
                    var idEquipment = context.GetArgument<int>("idEquipment");
                    return service.DeleteEquipmentFromTest(idTest, idEquipment).Result;
                });

            //Delete Specification From Test IdTest IdSpecification
            Field<GenericResponseType>("DeleteSpecificationFromTest")
                .Description("Delete Specification From Test")
                .Arguments(new QueryArguments(
                    new QueryArgument<IntGraphType> { Name = "idTest" },
                    new QueryArgument<IntGraphType> { Name = "idSpecification" }
                ))
                .Resolve(context =>
                {
                    var idTest = context.GetArgument<int>("idTest");
                    var idSpecification = context.GetArgument<int>("idSpecification");
                    return service.DeleteSpecificationFromTest(idTest, idSpecification).Result;
                });
            
            //Delete Attachment From Test IdTest IdAttachment
            Field<GenericResponseType>("DeleteAttachmentFromTest")
                .Description("Delete Attachment From Test")
                .Arguments(new QueryArguments(
                    new QueryArgument<IntGraphType> { Name = "idTest" },
                    new QueryArgument<IntGraphType> { Name = "idAttachment" }
                ))
                .Resolve(context =>
                {
                    var idTest = context.GetArgument<int>("idTest");
                    var idAttachment = context.GetArgument<int>("idAttachment");
                    return service.DeleteAttachmentFromTest(idTest, idAttachment).Result;
                });

            //Create Attachment From Test IdTest Attachment
            Field<GenericResponseType>("CreateAttachmentFromTest")
                .Description("Create Attachment From Test")
                .Arguments(new QueryArguments(
                    new QueryArgument<IntGraphType> { Name = "idTest" },
                    new QueryArgument<AttachmentInputType> { Name = "attachment" }
                ))
                .Resolve(context =>
                {
                    var idTest = context.GetArgument<int>("idTest");
                    var attachment = context.GetArgument<Attachment>("attachment");
                    return service.CreateAttachmentFromTest(idTest, attachment).Result;
                });


            //Delete Profile From Test IdTest
            Field<GenericResponseType>("DeleteProfileFromTest")
                .Description("Delete Profile From Test")
                .Arguments(new QueryArguments(
                    new QueryArgument<IntGraphType> { Name = "idTest" }
                ))
                .Resolve(context =>
                {
                    var idTest = context.GetArgument<int>("idTest");
                    return service.DeleteProfileFromTest(idTest).Result;
                });
            
            //Delete technician From Test IdTest IdEmployee
            Field<GenericResponseType>("DeleteTechnicianFromTest")
                .Description("Delete Technician From Test")
                .Arguments(new QueryArguments(
                    new QueryArgument<IntGraphType> { Name = "idTest" },
                    new QueryArgument<IntGraphType> { Name = "idEmployee" }
                ))
                .Resolve(context =>
                {
                    var idTest = context.GetArgument<int>("idTest");
                    var idEmployee = context.GetArgument<int>("idEmployee");
                    return service.DeleteTechnicianFromTest(idTest, idEmployee).Result;
                });


            //update start and end date of a test
            Field<GenericResponseType>("UpdateDatesTest")
                .Description("Update Dates Test")
                .Arguments(new QueryArguments(
                    new QueryArgument<IntGraphType> { Name = "idTest" },
                    new QueryArgument<StringGraphType> { Name = "startDate" },
                    new QueryArgument<StringGraphType> { Name = "endDate" }
                ))
                .Resolve(context =>
                {
                    var idTest = context.GetArgument<int>("idTest");
                    var startDate = context.GetArgument<string>("startDate");
                    var endDate = context.GetArgument<string>("endDate");
                    return service.UpdateDatesTest(idTest, startDate, endDate).Result;
                });
            

        }
    }
}
