using System.Linq.Expressions;
using Domain.DataBase;
using Domain.Models;
using Domain.Repositories;
using Shared.Response;
using System.Data;
using Microsoft.Data.SqlClient;

namespace Infrastructure.Repositories
{
    public class SamplesRepository : ISamplesRepository
    {
        private readonly ISQLDbConnect _dbConnect;

        public SamplesRepository(ISQLDbConnect dbConnect)
        {
            _dbConnect = dbConnect;
        }

        public async Task<Samples> AddAsync(Samples entity)
        {
            SqlParameter[] parameters = {
                new SqlParameter("@Quantity", entity.Quantity),
                new SqlParameter("@Weight", entity.Weight),
                new SqlParameter("@Size", entity.Size)
            };

            DataTable result = await _dbConnect.GetDataSPAsync("CreateSample", parameters);

            return new Samples
            {
                Id = result.Rows[0].Field<int>("Id"),
                Quantity = result.Rows[0].Field<int>("Quantity"),
                Weight = result.Rows[0].Field<decimal>("Weight"),
                Size = result.Rows[0].Field<decimal>("Size")
            };
        }

        public async Task<IEnumerable<Samples>> FindAsync(Expression<Func<Samples, bool>> predicate)
        {
            // Implementación futura para filtros avanzados
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Samples>> GetAllAsync()
        {
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

            return samplesList;
        }

        public async Task<Samples> GetByIdAsync(int id)
        {
            SqlParameter[] parameters = {
                new SqlParameter("@SampleId", id)
            };

            DataTable result = await _dbConnect.GetDataSPAsync("GetSampleById", parameters);

            if (result.Rows.Count == 0)
                throw new Exception("Sample not found");

            DataRow row = result.Rows[0];
            return new Samples
            {
                Id = row.Field<int>("Id"),
                Quantity = row.Field<int>("Quantity"),
                Weight = row.Field<decimal>("Weight"),
                Size = row.Field<decimal>("Size")
            };
        }

        public async Task<DBResponse> RemoveAsync(Samples entity)
        {
            SqlParameter[] parameters = {
                new SqlParameter("@SampleId", entity.Id)
            };

            DataTable result = await _dbConnect.GetDataSPAsync("DeleteSample", parameters);

            return new DBResponse
            {
                id = result.Rows[0].Field<int>("Id"),
                message = result.Rows[0].Field<string>("Message") ?? ""
            };
        }

        public async Task<DBResponse> UpdateAsync(Samples entity)
        {
            SqlParameter[] parameters = {
                new SqlParameter("@SampleId", entity.Id),
                new SqlParameter("@Quantity", entity.Quantity),
                new SqlParameter("@Weight", entity.Weight),
                new SqlParameter("@Size", entity.Size)
            };

            DataTable result = await _dbConnect.GetDataSPAsync("UpdateSample", parameters);

            return new DBResponse
            {
                id = result.Rows[0].Field<int>("Id"),
                message = result.Rows[0].Field<string>("Message") ?? ""
            };
        }
    }
}
