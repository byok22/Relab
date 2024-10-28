using Domain.Models.TestModels;
using Domain.Repositories;
using Shared.Response;
using System.Data;
using Microsoft.Data.SqlClient;
using Domain.DataBase;
using Domain.Models;

namespace Infrastructure.Repositories
{
    public class TestSpecificationsRepository : ITestSpecificationsRepository
    {
        private readonly ISQLDbConnect _dbConnect;

        public TestSpecificationsRepository(ISQLDbConnect dbConnect)
        {
            _dbConnect = dbConnect;
        }

        public async Task<DBResponse> AssignSpecificationToTest(TestSpecification testSpecification)
        {
            SqlParameter[] parameters = {
                new SqlParameter("@TestId", testSpecification.TestId),
                new SqlParameter("@SpecificationId", testSpecification.SpecificationId)
            };

            DataTable result = await _dbConnect.GetDataSPAsync("AssignSpecificationToTest", parameters);

            return new DBResponse
            {
                id = result.Rows[0].Field<int>("Id"),
                message = result.Rows[0].Field<string>("Message") ?? ""
            };
        }

        public async Task<DBResponse> RemoveSpecificationFromTest(TestSpecification testAttachment)
        {
            SqlParameter[] parameters = {
                new SqlParameter("@TestId", testAttachment.TestId),
                new SqlParameter("@SpecificationId", testAttachment.SpecificationId)
            };

            DataTable result = await _dbConnect.GetDataSPAsync("RemoveSpecificationFromTest", parameters);

            return new DBResponse
            {
                id = result.Rows[0].Field<int>("Id"),
                message = result.Rows[0].Field<string>("Message") ?? ""
            };
        }

        public async Task<List<Specification>> GetSpecificationsByTestId(int testId)
        {
            SqlParameter[] parameters = {
                new SqlParameter("@TestId", testId)
            };

            DataTable result = await _dbConnect.GetDataSPAsync("GetSpecificationsFromTest", parameters);

            List<Specification> specifications = new List<Specification>();

            foreach (DataRow row in result.Rows)
            {
                specifications.Add(new Specification
                {
                    Id = row.Field<int>("Id"),
                    SpecificationName = row.Field<string>("SpecificationName"),
                    Details = row.Field<string>("Details"),                 
                });
            }

            return specifications;
        }
    }
}
