using System.Data;
using Domain.Models.TestModels;
using Domain.Repositories;
using Shared.Response;
using Microsoft.Data.SqlClient;
using Domain.DataBase;
using Domain.Models.Generics;
using Domain.Models;

namespace Infrastructure.Repositories
{
    public class TestGenericUpdateRepository : ITestGenericUpdateRepository
    {
        private readonly ISQLDbConnect _dbConnect;

        public TestGenericUpdateRepository(ISQLDbConnect dbConnect)
        {
            _dbConnect = dbConnect;
        }

        public async Task<DBResponse> AssignGenericUpdateToTest(TestGenericUpdate genericUpdate)
        {
            SqlParameter[] parameters = {
                new SqlParameter("@TestId", genericUpdate.TestId),
                new SqlParameter("@GenericUpdateId", genericUpdate.GenericUpdateId)
            };

            DataTable result = await _dbConnect.GetDataSPAsync("AssignGenericUpdateToTest", parameters);

            return new DBResponse
            {
                id = result.Rows[0].Field<int>("Id"),
                message = result.Rows[0].Field<string>("Message") ?? ""
            };
        }

        public async Task<List<GenericUpdate>> GetGenericUpdatesByTestId(int testId)
        {
            SqlParameter[] parameters = {
                new SqlParameter("@TestId", testId)
            };
            DataTable dataTable = await _dbConnect.GetDataSPAsync("GetGenericUpdatesFromTest", parameters);
        
            List<GenericUpdate> genericUpdates = new List<GenericUpdate>();
        
            foreach (DataRow row in dataTable.Rows)
            {
                User user = new User();
                user.Id = row.Field<int>("UserId");
                genericUpdates.Add(new GenericUpdate
                {
                    Id = row.Field<int>("Id"),
                    Changes = row.Field<string>("Changes"),
                    Message = row.Field<string>("Message"),
                    User = user,                                   
                    UpdatedAt = row.Field<DateTime>("UpdatedAt")
                });
            }
            return genericUpdates;
        }

        public async Task<DBResponse> RemoveGenericUpdateFromTest(TestGenericUpdate genericUpdate)
        {
            SqlParameter[] parameters = {
                new SqlParameter("@TestId", genericUpdate.TestId),
                new SqlParameter("@GenericUpdateId", genericUpdate.GenericUpdateId)
            };

            DataTable result = await _dbConnect.GetDataSPAsync("RemoveGenericUpdateFromTest", parameters);

            return new DBResponse
            {
                id = result.Rows[0].Field<int>("Id"),
                message = result.Rows[0].Field<string>("Message") ?? ""
            };
        }
    }
}
