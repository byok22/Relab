using System.Linq.Expressions;
using Domain.Models;
using Shared.Response;
using Domain.Repositories;
using System.Data;
using Microsoft.Data.SqlClient;
using Domain.DataBase;
using Domain.Models.Equipments;

namespace Infrastructure.Repositories
{
    public class EquipmentsRepository : IEquipmentsRepository
    {
        private readonly ISQLDbConnect _dbConnect;

        public EquipmentsRepository(ISQLDbConnect dbConnect)
        {
            _dbConnect = dbConnect;
        }

        public async Task<Equipment> AddAsync(Equipment entity)
        {
            SqlParameter[] parameters = {

                new SqlParameter("@Name", entity.Name),
                new SqlParameter("@Description", entity.Description),
                new SqlParameter("@CalibrationDate", entity.CalibrationDate)
                
            };

            DataTable result = await _dbConnect.GetDataSPAsync("CreateEquipment", parameters);

            return new Equipment
            {
                Id = result.Rows[0].Field<int>("Id"),
                CalibrationDate = result.Rows[0].Field<DateTime>("CalibrationDate"),
                Description = result.Rows[0].Field<string>("Description"),
                Name = result.Rows[0].Field<string>("Name")
               
            };
        }

        public async Task<IEnumerable<Equipment>> FindAsync(Expression<Func<Equipment, bool>> predicate)
        {
            // Implementación futura para filtros avanzados
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Equipment>> GetAllAsync()
        {
            DataTable result = await _dbConnect.GetDataSPAsync("GetAllEquipments", null);
            List<Equipment> EquipmentsList = new List<Equipment>();

            foreach (DataRow row in result.Rows)
            {
                EquipmentsList.Add(new Equipment
                {
                    Id = row.Field<int>("Id"),
                    CalibrationDate = row.Field<DateTime>("CalibrationDate"),
                    Description = row.Field<string>("Description"),
                    Name = row.Field<string>("Name")
                });
            }

            return EquipmentsList;
        }

        public async Task<Equipment> GetByIdAsync(int id)
        {
            SqlParameter[] parameters = {
                new SqlParameter("@Id", id)
            };

            DataTable result = await _dbConnect.GetDataSPAsync("GetEquipmentById", parameters);

            if (result.Rows.Count == 0)
                throw new Exception("Equipment not found");

            DataRow row = result.Rows[0];
            return new Equipment
            {
                Id = row.Field<int>("Id"),
                Name = row.Field<string>("Name"),
                CalibrationDate = row.Field<DateTime>("CalibrationDate"),
                Description = row.Field<string>("Description"),
            };
        }

        public async Task<DBResponse> RemoveAsync(Equipment entity)
        {
            SqlParameter[] parameters = {
                new SqlParameter("@Id", entity.Id)
            };

            DataTable result = await _dbConnect.GetDataSPAsync("DeleteEquipment", parameters);

            return new DBResponse
            {
                id = result.Rows[0].Field<int>("Id"),
                message = result.Rows[0].Field<string>("Message") ?? ""
            };
        }

        public async Task<DBResponse> UpdateAsync(Equipment entity)
        {
            SqlParameter[] parameters = {
                new SqlParameter("@Id", entity.Id),
                new SqlParameter("@Name", entity.Name),
                new SqlParameter("@Description", entity.Description),
                new SqlParameter("@CalibrationDate", entity.CalibrationDate)
            };

            DataTable result = await _dbConnect.GetDataSPAsync("UpdateEquipment", parameters);

            return new DBResponse
            {
                id = result.Rows[0].Field<int>("Id"),
                message = result.Rows[0].Field<string>("Message") ?? ""
            };
        }
    }
}
