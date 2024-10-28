using Domain.Models.TestModels;
using Domain.Repositories;
using Shared.Response;

namespace Domain.Repositories
{
    
    public interface ITestAttachmentsRepository
    {
         public Task<DBResponse> AssignAttachmentToTest(TestAttachment testAttachment);   
         public Task<DBResponse> RemoveAttachmentFromTest(TestAttachment testAttachment);
        
    }
}