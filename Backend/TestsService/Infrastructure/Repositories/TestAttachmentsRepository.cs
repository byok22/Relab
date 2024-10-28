using Domain.DataBase;
using Shared.Response;
using Domain.Repositories;
using Microsoft.Data.SqlClient;
using Domain.Models.TestModels;
using System.Data;

namespace Infrastructure.Repositories
{
    public class TestAttachmentsRepository: ITestAttachmentsRepository
    {
         private readonly ISQLDbConnect _dbConnect;

        public TestAttachmentsRepository(ISQLDbConnect dbConnect)
        {
            _dbConnect = dbConnect;
        }

      
        public async Task<DBResponse> AssignAttachmentToTest(TestAttachment testAttachment)
        {
            SqlParameter[] parameters = {
                new SqlParameter("@TestId", testAttachment.TestId),
                new SqlParameter("@AttachmentId", testAttachment.AttachmentId)
            };

            DataTable result = await _dbConnect.GetDataSPAsync("AssignAttachmentToTest", parameters);

            return new DBResponse
            {
                id = result.Rows[0].Field<int>("id"),
                message = result.Rows[0].Field<string>("message")??""
            };
        }       
      

        public async Task<DBResponse> RemoveAttachmentFromTest(TestAttachment testAttachment)
        {
             SqlParameter[] parameters = {
                new SqlParameter("@TestId", testAttachment.TestId),
                new SqlParameter("@AttachmentId", testAttachment.AttachmentId)
            };

            DataTable result = await _dbConnect.GetDataSPAsync("RemoveAttachmentFromTest", parameters);

            return new DBResponse
            {
                id = result.Rows[0].Field<int>("id"),
                message = result.Rows[0].Field<string>("message")??""
            };
        }

    
    }
}