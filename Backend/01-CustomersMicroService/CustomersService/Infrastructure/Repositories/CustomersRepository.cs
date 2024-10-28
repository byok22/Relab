using System.Linq.Expressions;
using Domain.Models;
using Shared.Response;
using Domain.Repositories;
using System.Data;
using Microsoft.Data.SqlClient;
using Domain.DataBase;

namespace Infrastructure.Repositories
{
    public class CustomersRepository : ICustomersRepository
    {
        private readonly ISQLDbConnect _dbConnect;

        public CustomersRepository(ISQLDbConnect dbConnect) => _dbConnect = dbConnect;

        public async Task<Customer> AddAsync(Customer entity)
        {
            SqlParameter[] parameters = {
                new SqlParameter("@CustomerName", entity.CustomerName),
                new SqlParameter("@Division", entity.Division),
                new SqlParameter("@BuildingID", entity.BuildingID),
                new SqlParameter("@Building", entity.Building),
                new SqlParameter("@Available", entity.Available),
                new SqlParameter("@UpdatedBy", entity.UpdatedBy ?? (object)DBNull.Value),
                new SqlParameter("@CreatedBy", entity.CreatedBy ?? (object)DBNull.Value)
            };

            DataTable result = await _dbConnect.GetDataSPAsync("AddCustomer", parameters);

            return new Customer
            {
                Id = result.Rows[0].Field<int>("Id"),
                CustomerID = result.Rows[0].Field<Guid>("CustomerID").ToString(),
                CustomerName = result.Rows[0].Field<string>("CustomerName"),
                Division = result.Rows[0].Field<string>("Division"),
                BuildingID = result.Rows[0].Field<int>("BuildingID"),
                Building = result.Rows[0].Field<string>("Building"),
                Available = result.Rows[0].Field<bool>("Available"),
                CreatedAt = result.Rows[0].Field<DateTime>("CreatedAt"),
                UpdatedAt = result.Rows[0].Field<DateTime>("UpdatedAt"),
                UpdatedBy = result.Rows[0].Field<string>("UpdatedBy"),
                CreatedBy = result.Rows[0].Field<string>("CreatedBy")
            };
        }

        public async Task<IEnumerable<Customer>> FindAsync(Expression<Func<Customer, bool>> predicate)
        {
            // Implementación futura para filtros avanzados
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            DataTable result = await _dbConnect.GetDataSPAsync("GetAllCustomers", null);
            List<Customer> customersList = new List<Customer>();

            foreach (DataRow row in result.Rows)
            {
                customersList.Add(new Customer
                {
                    Id = row.Field<int>("Id"),
                    CustomerID = row.Field<Guid>("CustomerID").ToString(),
                    CustomerName = row.Field<string>("CustomerName"),
                    Division = row.Field<string>("Division"),
                    BuildingID = row.Field<int>("BuildingID"),
                    Building = row.Field<string>("Building"),
                    Available = row.Field<bool>("Available"),
                    CreatedAt = row.Field<DateTime>("CreatedAt"),
                    UpdatedAt = row.Field<DateTime>("UpdatedAt"),
                    UpdatedBy = row.Field<string>("UpdatedBy"),
                    CreatedBy = row.Field<string>("CreatedBy")
                });
            }

            return customersList;
        }

        //getbyCustomerID
        

        public async Task<Customer> GetByIdAsync(int id)
        {
            SqlParameter[] parameters = {
            new SqlParameter("@Id", id)
            };

            DataTable result = await _dbConnect.GetDataSPAsync("GetCustomerById", parameters);

            if (result.Rows.Count == 0 || result.Rows[0].Field<int?>("Id") == null)
            {
            return new Customer
            {
                Id = 0,
                CustomerID = string.Empty,
                CustomerName = string.Empty,
                Division = string.Empty,
                BuildingID = 0,
                Building = string.Empty,
                Available = false,
                CreatedAt = DateTime.MinValue,
                UpdatedAt = DateTime.MinValue,
                UpdatedBy = string.Empty,
                CreatedBy = string.Empty
            };
            }

            DataRow row = result.Rows[0];

            return new Customer
            {
            Id = row.Field<int>("Id"),
            CustomerID = row.Field<Guid>("CustomerID").ToString(),
            CustomerName = row.Field<string>("CustomerName"),
            Division = row.Field<string>("Division"),
            BuildingID = row.Field<int>("BuildingID"),
            Building = row.Field<string>("Building"),
            Available = row.Field<bool>("Available"),
            CreatedAt = row.Field<DateTime>("CreatedAt"),
            UpdatedAt = row.Field<DateTime>("UpdatedAt"),
            UpdatedBy = row.Field<string>("UpdatedBy"),
            CreatedBy = row.Field<string>("CreatedBy")
            };
        }

        public async Task<DBResponse> UpdateAsync(Customer entity)
        {
            SqlParameter[] parameters = {
                new SqlParameter("@Id", entity.Id),
                new SqlParameter("@CustomerName", entity.CustomerName),
                new SqlParameter("@Division", entity.Division),
                new SqlParameter("@BuildingID", entity.BuildingID),
                new SqlParameter("@Building", entity.Building),
                new SqlParameter("@Available", entity.Available),
                new SqlParameter("@UpdatedBy", entity.UpdatedBy ?? (object)DBNull.Value),
                new SqlParameter("@CreatedBy", entity.CreatedBy ?? (object)DBNull.Value)
            };

            DataTable result = await _dbConnect.GetDataSPAsync("UpdateCustomer", parameters);

            return new DBResponse
            {
                id = result.Rows[0].Field<int>("Id"),
                message = result.Rows[0].Field<string>("Message") ?? ""
            };
        }

        public async Task<DBResponse> RemoveAsync(Customer entity)
        {
            SqlParameter[] parameters = {
                new SqlParameter("@Id", entity.Id)
            };

            DataTable result = await _dbConnect.GetDataSPAsync("RemoveCustomer", parameters);

            return new DBResponse
            {
                id = result.Rows[0].Field<int>("Id"),
                message = result.Rows[0].Field<string>("Message") ?? ""
            };
        }

        public async Task<Customer> GetCustomerByCustomerIDAsync(string customerID)
        {
            SqlParameter[] parameters = {
            new SqlParameter("@CustomerID", customerID)
            };

            DataTable result = await _dbConnect.GetDataSPAsync("GetCustomerByUUID", parameters);

            if (result.Rows.Count == 0 || result.Rows[0].Field<int?>("Id") == null)
            {
            return new Customer
            {
                Id = 0,
                CustomerID = string.Empty,
                CustomerName = string.Empty,
                Division = string.Empty,
                BuildingID = 0,
                Building = string.Empty,
                Available = false,
                CreatedAt = DateTime.MinValue,
                UpdatedAt = DateTime.MinValue,
                UpdatedBy = string.Empty,
                CreatedBy = string.Empty
            };
            }

            DataRow row = result.Rows[0];

            return new Customer
            {
            Id = row.Field<int>("Id"),
            CustomerID = row.Field<Guid>("CustomerID").ToString(),
            CustomerName = row.Field<string>("CustomerName"),
            Division = row.Field<string>("Division"),
            BuildingID = row.Field<int>("BuildingID"),
            Building = row.Field<string>("Building"),
            Available = row.Field<bool>("Available"),
            CreatedAt = row.Field<DateTime>("CreatedAt"),
            UpdatedAt = row.Field<DateTime>("UpdatedAt"),
            UpdatedBy = row.Field<string>("UpdatedBy"),
            CreatedBy = row.Field<string>("CreatedBy")
            };
        }
    }
}