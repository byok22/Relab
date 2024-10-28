using GraphQL.Types;
using Presentation.Mutation;

//using Presentation.Mutation;
using Presentation.Queries;

namespace Presentation.Schemas
{
    public class RootSchema: Schema
    {
        public RootSchema(IServiceProvider serviceProvider): base(serviceProvider)
        {
            Query = serviceProvider.GetRequiredService<RootQuery>();
            Mutation = serviceProvider.GetRequiredService<RootMutation>();

            
        }
    }
}