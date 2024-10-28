using System.Linq.Expressions;
using Domain.DataBase;
using Domain.Models.Generics;
using Domain.Repositories;
using Shared.Response;
using System.Data;
using Microsoft.Data.SqlClient;
using Domain.Models;

namespace Infrastructure.Repositories
{
    public class GenericUpdateRepository : IGenericUpdateRepository
    {
        private readonly ISQLDbConnect _dbConnect;

        public GenericUpdateRepository(ISQLDbConnect dbConnect)
        {
            _dbConnect = dbConnect;
        }

        public async Task<GenericUpdate> AddAsync(GenericUpdate entity)
        {
            SqlParameter[] parameters = {
                new SqlParameter("@UpdatedAt", entity.UpdatedAt),
                new SqlParameter("@idUser", entity.User?.Id ?? (object)DBNull.Value),
                new SqlParameter("@Message", entity.Message ?? (object)DBNull.Value),
                new SqlParameter("@Changes", entity.Changes ?? (object)DBNull.Value)
            };

            DataTable result = await _dbConnect.GetDataSPAsync("CreateGenericUpdate", parameters);

            return new GenericUpdate
            {
                Id = result.Rows[0].Field<int>("Id"),
                UpdatedAt = result.Rows[0].Field<DateTime>("UpdatedAt"),
                User = new User { Id = result.Rows[0].Field<int>("idUser") }, // Assuming you have User class with Id property
                Message = result.Rows[0].Field<string>("Message"),
                Changes = result.Rows[0].Field<string>("Changes")
            };
        }

        public async Task<IEnumerable<GenericUpdate>> FindAsync(Expression<Func<GenericUpdate, bool>> predicate)
        {
            // Implementación futura para filtros avanzados
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<GenericUpdate>> GetAllAsync()
        {
            DataTable result = await _dbConnect.GetDataSPAsync("GetAllGenericUpdates", null);
            List<GenericUpdate> genericUpdateList = new List<GenericUpdate>();

            foreach (DataRow row in result.Rows)
            {
                genericUpdateList.Add(new GenericUpdate
                {
                    Id = row.Field<int>("Id"),
                    UpdatedAt = row.Field<DateTime>("UpdatedAt"),
                    User = new User { Id = row.Field<int>("idUser") }, // Assuming you have User class with Id property
                    Message = row.Field<string>("Message"),
                    Changes = row.Field<string>("Changes")
                });
            }

            return genericUpdateList;
        }

        public async Task<GenericUpdate> GetByIdAsync(int id)
        {
            SqlParameter[] parameters = {
                new SqlParameter("@GenericUpdateId", id)
            };

            DataTable result = await _dbConnect.GetDataSPAsync("GetGenericUpdateById", parameters);

            if (result.Rows.Count == 0)
                throw new Exception("Generic update not found");

            DataRow row = result.Rows[0];
            return new GenericUpdate
            {
                Id = row.Field<int>("Id"),
                UpdatedAt = row.Field<DateTime>("UpdatedAt"),
                User = new User { Id = row.Field<int>("idUser") }, // Assuming you have User class with Id property
                Message = row.Field<string>("Message"),
                Changes = row.Field<string>("Changes")
            };
        }

        public async Task<DBResponse> RemoveAsync(GenericUpdate entity)
        {
            SqlParameter[] parameters = {
                new SqlParameter("@GenericUpdateId", entity.Id)
            };

            DataTable result = await _dbConnect.GetDataSPAsync("DeleteGenericUpdate", parameters);

            return new DBResponse
            {
                id = result.Rows[0].Field<int>("Id"),
                message = result.Rows[0].Field<string>("Message") ?? ""
            };
        }

        public async Task<DBResponse> UpdateAsync(GenericUpdate entity)
        {
            SqlParameter[] parameters = {
                new SqlParameter("@GenericUpdateId", entity.Id),
                new SqlParameter("@UpdatedAt", entity.UpdatedAt),
                new SqlParameter("@idUser", entity.User?.Id ?? (object)DBNull.Value),
                new SqlParameter("@Message", entity.Message ?? (object)DBNull.Value),
                new SqlParameter("@Changes", entity.Changes ?? (object)DBNull.Value)
            };

            DataTable result = await _dbConnect.GetDataSPAsync("UpdateGenericUpdate", parameters);

            return new DBResponse
            {
                id = result.Rows[0].Field<int>("Id"),
                message = result.Rows[0].Field<string>("Message") ?? ""
            };
        }
    }
}
