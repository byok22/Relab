using GraphQL.Types;
using Domain.Models.Equipments;

namespace Presentation.Types
{
    public class EquipmentInputType : InputObjectGraphType<Equipment>
    {
        public EquipmentInputType()
        {
            Name = "EquipmentInput";
            Field<IntGraphType>("id").Description("ID of the equipment");
            Field<StringGraphType>("name").Description("Name of the equipment");
            Field<StringGraphType>("description").Description("Description of the equipment");
            Field<DateTimeGraphType>("calibrationDate").Description("Date when the equipment was last calibrated");
        }
    }
}
