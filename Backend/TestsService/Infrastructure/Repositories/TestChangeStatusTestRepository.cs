using Domain.Models;
using Domain.Repositories;
using Shared.Response;
using Domain.DataBase;
using Microsoft.Data.SqlClient;
using System.Data;
using Domain.Models.TestModels;
using Domain.Enums;

namespace Infrastructure.Repositories
{
    public class TestChangeStatusTestRepository : ITestChangeStatusTestRepository
    {
        private readonly ISQLDbConnect _dbConnect;

        public TestChangeStatusTestRepository(ISQLDbConnect dbConnect)
        {
            _dbConnect = dbConnect;
        }

        public async Task<DBResponse> AssignChangeStatusToTest(TestChangeStatusTest changeStatus)
        {
                     
             SqlParameter[] parameters = {
                new SqlParameter("@TestId", changeStatus.TestId),
                new SqlParameter("@ChangeStatusTestId", changeStatus.ChangeStatusTestId)
            };

            DataTable result = await _dbConnect.GetDataSPAsync("AssignChangeStatusToTest", parameters);

            return new DBResponse
            {
                id = result.Rows[0].Field<int>("Id"),
                message = result.Rows[0].Field<string>("Message") ?? ""
            };
        }

        public async Task<List<ChangeStatusTest>> GetStatusByTestId(int testId)
        {
            SqlParameter[] parameters = {
                new SqlParameter("@TestId", testId)
            };
        
            DataTable result = await _dbConnect.GetDataSPAsync("GetChangeStatusFormTest", parameters);
        
            List<ChangeStatusTest> changeStatus = new List<ChangeStatusTest>();
            foreach (DataRow row in result.Rows)
            {
                var attachment = new Attachment();
                attachment.Id = row.Field<int>("AttachmentId");
                changeStatus.Add(new ChangeStatusTest
                {
                   
                   Id = row.Field<int>("Id"),
                    Message = row.Field<string>("Message")!,
                   attachment = attachment,
                    status = row.Field<string>("Status") != null ? (TestStatusEnum)Enum.Parse(typeof(TestStatusEnum), row.Field<string>("Status")) : TestStatusEnum.New,
        
        
                });
            }
            return changeStatus;
        }

        public async Task<DBResponse> RemoveChangeStatusFromTest(TestChangeStatusTest changeStatus)
        {
            SqlParameter[] parameters = {
                new SqlParameter("@TestId", changeStatus.TestId), // Ajustar si la propiedad correcta es idUser o algo más
                new SqlParameter("@ChangeStatusTestId", (int)changeStatus.ChangeStatusTestId) // Convertir el enum a int
            };

            DataTable result = await _dbConnect.GetDataSPAsync("RemoveChangeStatusFromTest", parameters);

            return new DBResponse
            {
                id = result.Rows[0].Field<int>("Id"),
                message = result.Rows[0].Field<string>("Message") ?? ""
            };
        }
    }
}
