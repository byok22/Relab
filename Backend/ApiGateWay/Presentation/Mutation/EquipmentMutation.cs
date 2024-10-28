using Domain.Models.Equipments;
using Domain.Services;
using GraphQL;
using GraphQL.Types;
using Presentation.Types;

namespace Presentation.Mutation
{
    public class EquipmentMutation: ObjectGraphType
    {
        public EquipmentMutation( IEquipmentsMicroServices equipmentsService)
        {
              Field<GenericResponseType>("addEquipment")
                .Description("Add Equipment") 
                .Arguments(new QueryArguments(
                    new QueryArgument<EquipmentInputType>{Name = "equipment"}
                ) )              
                .Resolve(context =>
                { 
                    var equipment = context.GetArgument<Equipment>("equipment");
                    return  equipmentsService.AddEquipment(equipment).Result;
                });

                Field<GenericResponseType>("updateEquipment")
                .Description("Update Equipment") 
                .Arguments(new QueryArguments(                   
                    new QueryArgument<EquipmentInputType>{Name = "equipment"}
                ) )              
                .Resolve(context =>
                {                  
                    var equipment = context.GetArgument<Equipment>("equipment");
                    return   equipmentsService.PatchEquipment(equipment).Result;
                });

                 Field<StringGraphType>("deleteEquipment")
                .Description("Delete Equipment") 
                .Arguments(new QueryArguments(
                     new QueryArgument<IntGraphType>{Name = "equipmentId"}                   
                ) )              
                .Resolve(context =>
                {
                    var equipmentId = context.GetArgument<int>("equipmentId");
                    Task.WhenAll(equipmentsService.RemoveEquipment(equipmentId));
                    return "The Equipment against this Id"+ equipmentId.ToString() + "has been deleted";                    
                });
        }
        
    }
}