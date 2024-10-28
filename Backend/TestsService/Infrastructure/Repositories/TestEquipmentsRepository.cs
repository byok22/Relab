using Domain.Models;
using Domain.Repositories;
using Shared.Response;
using Domain.DataBase;
using Microsoft.Data.SqlClient;
using System.Data;
using Domain.Models.TestModels;
using Domain.Enums;
using Domain.Models.Equipments;

namespace Infrastructure.Repositories
{
    public class TestEquipmentsRepository : ITestEquipmentsRepository
    {
        private readonly ISQLDbConnect _dbConnect;

        public TestEquipmentsRepository(ISQLDbConnect dbConnect)
        {
            _dbConnect = dbConnect;
        }

        public async Task<DBResponse> AssignEquipmentToTest(TestEquipment Equipment)
        {
                     
             SqlParameter[] parameters = {
                new SqlParameter("@TestId", Equipment.TestId),
                new SqlParameter("@EquipmentId", Equipment.EquipmentId)
            };

            DataTable result = await _dbConnect.GetDataSPAsync("AssignEquipmentToTest", parameters);

            return new DBResponse
            {
                id = result.Rows[0].Field<int>("Id"),
                message = result.Rows[0].Field<string>("Message") ?? ""
            };
        }

        public async Task<List<Equipment>> GetEquipmentByTestId(int testId)
        {
            SqlParameter[] parameters = {
                new SqlParameter("@TestId", testId)
            };
        
            DataTable result = await _dbConnect.GetDataSPAsync("GetEquipmentsFromTest", parameters);
        
            List<Equipment> equipments = new List<Equipment>();
            foreach (DataRow row in result.Rows)
            {
                
                equipments.Add(new Equipment
                {
                   
                   Id = row.Field<int>("Id"),
                  CalibrationDate = row.Field<DateTime>("CalibrationDate"),
                  Description = row.Field<string>("Description"),
                  Name = row.Field<string>("Name"),        

                });
            }
            return equipments;
        }

        public async Task<DBResponse> RemoveEquipmentFromTest(TestEquipment Equipment)
        {
            SqlParameter[] parameters = {
                new SqlParameter("@TestId", Equipment.TestId), 
                new SqlParameter("@EquipmentId", (int)Equipment.EquipmentId) 
            };

            DataTable result = await _dbConnect.GetDataSPAsync("RemoveEquipmentFromTest", parameters);

            return new DBResponse
            {
                id = result.Rows[0].Field<int>("Id"),
                message = result.Rows[0].Field<string>("Message") ?? ""
            };
        }
    }
}
