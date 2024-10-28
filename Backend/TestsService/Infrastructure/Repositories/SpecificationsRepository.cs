using System.Linq.Expressions;
using Domain.Models;
using Shared.Response;
using Domain.Repositories;
using System.Data;
using Microsoft.Data.SqlClient;
using Domain.DataBase;

namespace Infrastructure.Repositories
{
    public class SpecificationsRepository : ISpecificationsRepository
    {
        private readonly ISQLDbConnect _dbConnect;

        public SpecificationsRepository(ISQLDbConnect dbConnect)
        {
            _dbConnect = dbConnect;
        }

        public async Task<Specification> AddAsync(Specification entity)
        {
            SqlParameter[] parameters = {
                new SqlParameter("@SpecificationName", entity.SpecificationName),
                new SqlParameter("@Details", entity.Details)
            };
            _dbConnect.CloseConnection();

            DataTable result =  _dbConnect.GetDataSP("CreateSpecification", parameters);
            var resultado =  new Specification();
            resultado.Id = result.Rows[0].Field<int>("Id");
            resultado.SpecificationName = result.Rows[0].Field<string>("SpecificationName");
            resultado.Details = result.Rows[0].Field<string>("Details");

            return resultado;
        }

        public async Task<IEnumerable<Specification>> FindAsync(Expression<Func<Specification, bool>> predicate)
        {
            // Implementación futura para filtros avanzados
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Specification>> GetAllAsync()
        {
            DataTable result = await _dbConnect.GetDataSPAsync("GetAllSpecifications", null);
            List<Specification> specificationsList = new List<Specification>();

            foreach (DataRow row in result.Rows)
            {
                specificationsList.Add(new Specification
                {
                    Id = row.Field<int>("Id"),
                    SpecificationName = row.Field<string>("SpecificationName"),
                    Details = row.Field<string>("Details")
                });
            }

            return specificationsList;
        }

        public async Task<Specification> GetByIdAsync(int id)
        {
            SqlParameter[] parameters = {
                new SqlParameter("@SpecificationId", id)
            };

            DataTable result = await _dbConnect.GetDataSPAsync("GetSpecificationById", parameters);

            if (result.Rows.Count == 0)
                throw new Exception("Specification not found");

            DataRow row = result.Rows[0];
            return new Specification
            {
                Id = row.Field<int>("Id"),
                SpecificationName = row.Field<string>("SpecificationName"),
                Details = row.Field<string>("Details")
            };
        }

        public async Task<DBResponse> RemoveAsync(Specification entity)
        {
            SqlParameter[] parameters = {
                new SqlParameter("@SpecificationId", entity.Id)
            };

            DataTable result = await _dbConnect.GetDataSPAsync("DeleteSpecification", parameters);

            return new DBResponse
            {
                id = result.Rows[0].Field<int>("Id"),
                message = result.Rows[0].Field<string>("Message") ?? ""
            };
        }

        public async Task<DBResponse> UpdateAsync(Specification entity)
        {
            SqlParameter[] parameters = {
                new SqlParameter("@SpecificationId", entity.Id),
                new SqlParameter("@SpecificationName", entity.SpecificationName),
                new SqlParameter("@Details", entity.Details)
            };

            DataTable result = await _dbConnect.GetDataSPAsync("UpdateSpecification", parameters);

            return new DBResponse
            {
                id = result.Rows[0].Field<int>("Id"),
                message = result.Rows[0].Field<string>("Message") ?? ""
            };
        }
    }
}
