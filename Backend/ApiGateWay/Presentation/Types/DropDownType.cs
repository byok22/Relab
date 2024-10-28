using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Models.Generics;
using GraphQL.Types;

namespace ApiGateWay.Presentation.Types
{
    public class DropDownType: ObjectGraphType<DropDown>
    {
        public DropDownType()
        {
            Field(x => x.Id).Description("ID of the drop down");
            Field(x => x.Text).Description("Name of the drop down");
        }
        
    }
}