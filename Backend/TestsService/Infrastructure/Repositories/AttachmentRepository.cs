using System.Linq.Expressions;
using Domain.DataBase;
using Domain.Models;
using Domain.Repositories;
using Shared.Response;
using System.Data;

using Microsoft.Data.SqlClient;

namespace Infrastructure.Repositories
{
    public class AttachmentRepository : IAttachmentRepository
    {
        private readonly ISQLDbConnect _dbConnect;

        public AttachmentRepository(ISQLDbConnect dbConnect)
        {
            _dbConnect = dbConnect;
        }

        public async Task<Attachment> AddAsync(Attachment entity)
        {
            SqlParameter[] parameters = {
                new SqlParameter("@Name", entity.Name),
                new SqlParameter("@Location", entity.Location),
                new SqlParameter("@Url", entity.Url)
            };

            DataTable result = await _dbConnect.GetDataSPAsync("CreateAttachment", parameters);

            return new Attachment
            {
                Id = result.Rows[0].Field<int>("Id"),
                Name = result.Rows[0].Field<string>("Name")??"",
                Location = result.Rows[0].Field<string>("Location")??"",
                Url = result.Rows[0].Field<string>("Url")??""
            };
        }

       

        public async Task<IEnumerable<Attachment>> FindAsync(Expression<Func<Attachment, bool>> predicate)
        {
            // Para filtros avanzados, necesitarías construir una consulta SQL dinámica basada en la expresión.
            // Aquí se omite por simplicidad, pero en una aplicación real, usarías una consulta dinámica o LINQ-to-SQL.
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Attachment>> GetAllAsync()
        {
            DataTable result = await _dbConnect.GetDataSPAsync("GetAllAttachments", null);
            List<Attachment> attachments = new List<Attachment>();

            foreach (DataRow row in result.Rows)
            {
                attachments.Add(new Attachment
                {
                    Name = row.Field<string>("Name")??"",
                    Location = row.Field<string>("Location")??"",
                    Url = row.Field<string>("Url")??""
                });
            }

            return attachments;
        }

        public async Task<List<Attachment>> GetAttachmentsByTestID(int testID)
        {
            SqlParameter[] parameters = {
                new SqlParameter("@TestId", testID)
            };

            DataTable result = await _dbConnect.GetDataSPAsync("GetAttachmentsOfTest", parameters);
            List<Attachment> attachments = new List<Attachment>();

            foreach (DataRow row in result.Rows)
            {
                if(row.ItemArray[1]!= null && row.ItemArray[1].ToString().Contains("No attachments found for the specified test"))
                    continue;
                attachments.Add(new Attachment
                {
                    Name = row.Field<string>("Name")??"",
                    Location = row.Field<string>("Location")??"",
                    Url = row.Field<string>("Url")??"",
                    Id = row.Field<int>("Id")
                });
            }

            return attachments;
        }

        public async Task<Attachment> GetByIdAsync(int id)
        {
            SqlParameter[] parameters = {
                new SqlParameter("@Id", id)
            };

            DataTable result = await _dbConnect.GetDataSPAsync("GetAttachmentById", parameters);

            if (result.Rows.Count == 0)
                throw new Exception("Not Found");

            DataRow row = result.Rows[0];
            return new Attachment
            {
                Name = row.Field<string>("Name")??"",
                Location = row.Field<string>("Location")??"",
                Url = row.Field<string>("Url")??""
            };
        }

        public async Task<DBResponse> RemoveAsync(Attachment entity)
        {
            SqlParameter[] parameters = {
                new SqlParameter("@Name", entity.Name),
                new SqlParameter("@File", entity.Location),
                new SqlParameter("@Url", entity.Url)
            };

            DataTable result = await _dbConnect.GetDataSPAsync("UpdateAttachment", parameters);

            return new DBResponse
            {
                id = result.Rows[0].Field<int>("id"),
                message = result.Rows[0].Field<string>("message")??""
            };
        }

        public async Task<DBResponse> UpdateAsync(Attachment entity)
        {
            SqlParameter[] parameters = {
                new SqlParameter("@Name", entity.Name),
                new SqlParameter("@File", entity.Location),
                new SqlParameter("@Url", entity.Url)
            };

            DataTable result = await _dbConnect.GetDataSPAsync("UpdateAttachment", parameters);

            return new DBResponse
            {
                id = result.Rows[0].Field<int>("id"),
                message = result.Rows[0].Field<string>("message")??""
            };
        }
    }
}
