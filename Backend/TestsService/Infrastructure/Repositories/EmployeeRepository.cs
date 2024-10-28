using System.Linq.Expressions;
using Domain.DataBase;
using Domain.Models.Employees;
using Domain.Repositories;
using Shared.Response;
using System.Data;
using Microsoft.Data.SqlClient;

namespace Infrastructure.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ISQLDbConnect _dbConnect;

        public EmployeeRepository(ISQLDbConnect dbConnect)
        {
            _dbConnect = dbConnect;
        }

        public async Task<Employee> AddAsync(Employee entity)
        {
            SqlParameter[] parameters = {
                new SqlParameter("@EmployeeNumber", entity.EmployeeNumber),
                new SqlParameter("@Name", entity.Name),
                new SqlParameter("@EmployeeType", entity.EmployeeType.ToString())
            };

            DataTable result = await _dbConnect.GetDataSPAsync("CreateEmployee", parameters);

            return new Employee
            {
                Id = result.Rows[0].Field<int>("Id"),
                EmployeeNumber = result.Rows[0]?.Field<string>("EmployeeNumber") ?? string.Empty,
                Name = result.Rows[0]?.Field<string>("Name") ?? string.Empty,
                EmployeeType = Enum.Parse<EmployeeTypeEnum>(result.Rows[0].Field<string>("EmployeeType") ?? nameof(EmployeeTypeEnum.Technician))
            };
        }

        public async Task<IEnumerable<Employee>> FindAsync(Expression<Func<Employee, bool>> predicate)
        {
            // Implementación futura para filtros avanzados
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            DataTable result = await _dbConnect.GetDataSPAsync("GetAllEmployees", null);
            List<Employee> employeeList = new List<Employee>();

            foreach (DataRow row in result.Rows)
            {
                employeeList.Add(new Employee
                {
                    Id = row.Field<int>("Id"),
                    EmployeeNumber = row.Field<string>("EmployeeNumber")?.ToString() ?? string.Empty,
                    Name = row.Field<string>("Name") ?? string.Empty,
                    EmployeeType = Enum.TryParse<EmployeeTypeEnum>(result.Rows[0].Field<string>("EmployeeType"), true, out var types) ? types : EmployeeTypeEnum.Engineer,

                 

                    
                });
            }

            return employeeList;
        }

        public async Task<List<Employee>> GetByEmployeebyType(EmployeeTypeEnum employeeType)
        {
            SqlParameter[] parameters = {
                new SqlParameter("@EmployeeType", employeeType.ToString().ToUpper())
            };
            DataTable result = await _dbConnect.GetDataSPAsync("GetEmployeeByType", parameters);

            List<Employee> employeeList = new List<Employee>();
            foreach (DataRow row in result.Rows)
            {
                employeeList.Add(new Employee
                {
                    Id = row.Field<int>("Id"),
                    EmployeeNumber = row.Field<string>("EmployeeNumber").ToString(),
                    Name = row.Field<string>("Name"),
                    EmployeeType = Enum.TryParse<EmployeeTypeEnum>(result.Rows[0].Field<string>("EmployeeType"), true, out var types) ? types : EmployeeTypeEnum.Engineer,

                 

                    
                });
            }

            return employeeList;



        }

        public async Task<Employee> GetByEmployeeNumberAsync(string employeeNumber)
        {
             SqlParameter[] parameters = {
             
                new SqlParameter("@EmployeeNumber", employeeNumber)};
                DataTable result = await _dbConnect.GetDataSPAsync("GetEmployeeByEmployeeNumber", parameters);
           //DataTable result = await _dbConnect.GetDataSPAsync("Get", parameters);

            if (result.Rows.Count == 0)
                throw new Exception("Employee not found");

            

            DataRow row = result.Rows[0];
            if (row.ItemArray[1] != null && row.ItemArray[1].ToString().Contains("Employee not found"))
                throw new Exception("Employee not found");
                
            return new Employee
            {
                Id = row.Field<int>("Id"),
                EmployeeNumber = row.Field<string>("EmployeeNumber").ToString(),
                Name = row.Field<string>("Name"),
                EmployeeType = Enum.TryParse<EmployeeTypeEnum>(result.Rows[0].Field<string>("EmployeeType"), true, out var types) ? types : EmployeeTypeEnum.Engineer,
            };

        }

        public async Task<Employee> GetByIdAsync(int id)
        {
            SqlParameter[] parameters = {
                new SqlParameter("@EmployeeId", id)
            };

            DataTable result = await _dbConnect.GetDataSPAsync("GetEmployeeById", parameters);

            if (result.Rows.Count == 0)
                throw new Exception("Employee not found");

            DataRow row = result.Rows[0];
            return new Employee
            {
                Id = row.Field<int>("Id"),
                EmployeeNumber = row.Field<string>("EmployeeNumber").ToString(),
                Name = row.Field<string>("Name"),
                EmployeeType =Enum.TryParse<EmployeeTypeEnum>(result.Rows[0].Field<string>("EmployeeType"), true, out var types) ? types : EmployeeTypeEnum.Engineer,
            };
        }

        public async Task<DBResponse> RemoveAsync(Employee entity)
        {
            SqlParameter[] parameters = {
                new SqlParameter("@EmployeeId", entity.Id)
            };

            DataTable result = await _dbConnect.GetDataSPAsync("DeleteEmployee", parameters);

            return new DBResponse
            {
                id = result.Rows[0].Field<int>("Id"),
                message = result.Rows[0].Field<string>("Message") ?? ""
            };
        }

        public async Task<DBResponse> UpdateAsync(Employee entity)
        {
            SqlParameter[] parameters = {
                new SqlParameter("@EmployeeId", entity.Id),
                new SqlParameter("@EmployeeNumber", entity.EmployeeNumber),
                new SqlParameter("@Name", entity.Name),
                new SqlParameter("@EmployeeType", entity.EmployeeType)
            };

            DataTable result = await _dbConnect.GetDataSPAsync("UpdateEmployee", parameters);

            return new DBResponse
            {
                id = result.Rows[0].Field<int>("Id"),
                message = result.Rows[0].Field<string>("Message") ?? ""
            };
        }
    }
}
