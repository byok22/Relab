using System.Data;
using Microsoft.Data.SqlClient;
using Domain.DataBase;

namespace Infrastructure.Persitence
{
    public class SQLDbConnect : ISQLDbConnect
    {
        private SqlConnection _conn;

        public SQLDbConnect(SqlConnection conn)
        {
            _conn = conn;
        }

        public void CloseConnection()
        {
            if (_conn.State == ConnectionState.Open)
            {
                _conn.Close();
            }
        }

        public SqlConnection GetConnection()
        {
            if (_conn.State == ConnectionState.Closed)
            {
                _conn.Open();
            }
            return _conn;
        }

        public void ExecuteNonQuery(string query)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(query, GetConnection());
                cmd.ExecuteNonQuery();
                CloseConnection();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to execute non-query: " + ex.Message);
            }
        }

        public async Task ExecuteNonQueryAsync(string query)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(query, GetConnection());
                await cmd.ExecuteNonQueryAsync().ConfigureAwait(false);
                CloseConnection();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to execute non-query asynchronously: " + ex.Message);
            }
        }

        public DataTable GetData(string query)
        {
            try
            {
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(query, GetConnection());
                da.Fill(dt);
                CloseConnection();
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to get data: " + ex.Message);
            }
        }

        public async Task<DataTable> GetDataAsync(string query)
        {
            try
            {
                DataTable dt = new DataTable();
                SqlCommand cmd = new SqlCommand(query, GetConnection());
                using (SqlDataReader reader = await cmd.ExecuteReaderAsync().ConfigureAwait(false))
                {
                    dt.Load(reader);
                }
                CloseConnection();
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to get data asynchronously: " + ex.Message);
            }
        }

        public DataTable GetDataSP(string spName, SqlParameter[] param)
        {
            try
            {
                DataTable dt = new DataTable();
                SqlCommand cmd = new SqlCommand(spName, GetConnection());
                cmd.CommandType = CommandType.StoredProcedure;
                if (param != null)
                    cmd.Parameters.AddRange(param);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                CloseConnection();
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to get data from stored procedure: " + ex.Message);
            }
        }

      public async Task<DataTable> GetDataSPAsync(string spName, SqlParameter[] param)
        {
            try
            {
                DataTable dt = new DataTable();
              
                    SqlCommand cmd = new SqlCommand(spName, GetConnection());
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (param != null)
                        cmd.Parameters.AddRange(param);
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        dt.Load(reader);
                    }
                
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener datos de procedimiento almacenado de forma asíncrona: " + ex.Message);
            }
        }

        public void SaveData(string query)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(query, GetConnection());
                cmd.ExecuteNonQuery();
                CloseConnection();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to save data: " + ex.Message);
            }
        }

        public async Task SaveDataAsync(string query)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(query, GetConnection());
                await cmd.ExecuteNonQueryAsync();
                CloseConnection();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to save data asynchronously: " + ex.Message);
            }
        }
    }
}