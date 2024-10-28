using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Models;
using GraphQL.Types;

namespace ApiGateWay.Presentation.Types.Inputs
{
    public class SamplesInputType: InputObjectGraphType<Samples>
    {
        public SamplesInputType()
        {
            Name = "SamplesInput";
            Description = "Input type for Samples";

            Field<DecimalGraphType>("Quantity")
               .Description("The quantity of samples.");
             Field<DecimalGraphType>("Weight")
                .Description("The weight of the samples.");
             Field<DecimalGraphType>("Size")
               .Description("The size of the samples.");         
        }
    }
}



