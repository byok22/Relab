using GraphQL.Types;
using Domain.Models.Equipments;

namespace Presentation.Types
{
    public class EquipmentType : ObjectGraphType<Equipment>
    {
        public EquipmentType()
        {
            Field(x => x.Id).Description("ID of the equipment");
            Field(x => x.Name).Description("Name of the equipment");
            Field(x => x.Description).Description("Description of the equipment");
            Field(x => x.CalibrationDate).Description("Date when the equipment was last calibrated");
        }
    }
}
