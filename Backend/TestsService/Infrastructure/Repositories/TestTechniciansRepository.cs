using Domain.Models.TestModels;
using Domain.Repositories;
using Shared.Response;
using System.Data;
using Microsoft.Data.SqlClient;
using Domain.DataBase;
using Shared.Dtos;
using Domain.Models.Employees;

namespace Infrastructure.Repositories
{
    public class TestTechniciansRepository : ITestTechniciansRepository
    {
        private readonly ISQLDbConnect _dbConnect;

        public TestTechniciansRepository(ISQLDbConnect dbConnect)
        {
            _dbConnect = dbConnect;
        }

        public async Task<DBResponse> AssignTechnicianToTest(TestTechnicians testTechnician)
        {
            SqlParameter[] parameters = {
                new SqlParameter("@TestId", testTechnician.TestId),
                new SqlParameter("@EmployeeId", testTechnician.EmployeeId)
            };

            DataTable result = await _dbConnect.GetDataSPAsync("AssignTechnicianToTest", parameters);

            return new DBResponse
            {
                id = result.Rows[0].Field<int>("Id"),
                message = result.Rows[0].Field<string>("Message") ?? ""
            };
        }

        /*
         DataTable result = await _dbConnect.GetDataSPAsync("GetAllSamples", null);
            List<Samples> samplesList = new List<Samples>();

            foreach (DataRow row in result.Rows)
            {
                samplesList.Add(new Samples
                {
                    Id = row.Field<int>("Id"),
                    Quantity = row.Field<int>("Quantity"),
                    Weight = row.Field<decimal>("Weight"),
                    Size = row.Field<decimal>("Size")
                });
            }


        */

        public async Task<List<EmployeeDto>> GetTechniciansFromTest(int idTest)
        {
            SqlParameter[] sqlParameter = {
                new SqlParameter("@TestId", idTest)
            };
            
            DataTable result =await  _dbConnect.GetDataSPAsync("GetTechniciansFormATest", sqlParameter);
            List<EmployeeDto> employeeList = new List<EmployeeDto>();
            foreach (DataRow row in result.Rows)
            {
                employeeList.Add(new EmployeeDto
                {
                    Id = row.Field<int>("Id"),
                    EmployeeNumber = row.Field<string>("EmployeeNumber"),
                    Name = row.Field<string>("Name"),
                    //EmployeeType = Enum.Parse<EmployeeType>(result.Rows[0].Field<string>("EmployeeType") ?? nameof(EmployeeTypeEnum.Technician))
                    //Enum.TryParse<TestStatusEnum>(testAr.Status, true, out var status) ? status : TestStatusEnum.New,
                    EmployeeType = EmployeeTypeEnum.Technician

                });
            }
            return employeeList;
        }

        public async Task<DBResponse> RemoveTechnicianFromTest(TestTechnicians testTechnician)
        {
            SqlParameter[] parameters = {
                new SqlParameter("@TestId", testTechnician.TestId),
                new SqlParameter("@EmployeeId", testTechnician.EmployeeId)
            };

            DataTable result = await _dbConnect.GetDataSPAsync("RemoveTechnicianFromTest", parameters);

            return new DBResponse
            {
                id = result.Rows[0].Field<int>("Id"),
                message = result.Rows[0].Field<string>("Message") ?? ""
            };
        }
    }
}
