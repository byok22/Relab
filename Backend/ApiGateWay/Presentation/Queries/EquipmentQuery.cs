

using ApiGateWay.Presentation.Types;
using Domain.Services;
using GraphQL;

using GraphQL.Types;
using Presentation.Services;
using Presentation.Types;

namespace Presentation.Queries
{
    public class EquipmentQuery : ObjectGraphType
    {
        public EquipmentQuery(IEquipmentsMicroServices equipmentsService)
        {
            Field<ListGraphType<EquipmentType>>("equipments")
                .Description("All Equipments")               
                .Resolve(context =>
                {
                   return  equipmentsService.GetAllEquipments().Result;
                });

            Field<EquipmentType>("equipment")
                .Description("Retrieve equipment by ID") 
                .Arguments(new QueryArguments(
                    new QueryArgument<IntGraphType>{Name = "equipmentId"}
                ) )              
                .Resolve(context =>
                {
                   return  equipmentsService.GetEquipmentById(context.GetArgument<int>("equipmentId")).Result;
                });

            Field<ListGraphType<DropDownType>>("equipmentDropDown")
                .Description("Equipment Drop Down Status")               
                .Resolve(context =>
                {
                   return  equipmentsService.GetEquipmentsStatus().Result;
                });
        }
    }
}
