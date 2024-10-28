using Domain.Models;
using Domain.Repositories;
using Shared.Response;
using System.Data;
using Microsoft.Data.SqlClient;
using Domain.DataBase;
using System.Linq.Expressions;
using Domain.Enums;

namespace Infrastructure.Repositories
{
    public class ChangeStatusTestRepository : IChangeStatusTestRepository
    {
        private readonly ISQLDbConnect _dbConnect;

        public ChangeStatusTestRepository(ISQLDbConnect dbConnect)
        {
            _dbConnect = dbConnect;
        }

        public async Task<ChangeStatusTest> AddAsync(ChangeStatusTest entity)
        {
            SqlParameter[] parameters = {
                new SqlParameter("@Status", entity.status.ToString().ToUpper()), // Asumiendo que TestStatusEnum se puede convertir a string
                new SqlParameter("@Message", entity.Message),
                new SqlParameter("@AttachmentId", entity.attachment != null ? (object)entity.attachment.Id : DBNull.Value), // Ajusta según la estructura de tu Attachment
                new SqlParameter("@IdUser", entity.idUser),
                new SqlParameter("@TestId", entity.idTest)
            };

            DataTable result = await _dbConnect.GetDataSPAsync("CreateChangeStatusTest", parameters);

            return new ChangeStatusTest
            {
                Id = result.Rows[0].Field<int>("Id"),
                status = Enum.TryParse<TestStatusEnum>(result.Rows[0].Field<string>("Status"), true, out var status) ? status : TestStatusEnum.New,                
                Message = result.Rows[0].Field<string>("Message") ?? "",
                attachment = result.Rows[0].IsNull("AttachmentId") ? new Attachment() : new Attachment { Id = result.Rows[0].Field<int>("AttachmentId") }, // Create new Attachment if AttachmentId exists
                idUser = result.Rows[0].Field<int>("idUser")
            };
        }

        public async Task<IEnumerable<ChangeStatusTest>> GetAllAsync()
        {
            DataTable result = await _dbConnect.GetDataSPAsync("GetAllChangeStatusTests", null);
            List<ChangeStatusTest> changeStatusTestsList = new List<ChangeStatusTest>();

            foreach (DataRow row in result.Rows)
            {
                changeStatusTestsList.Add(new ChangeStatusTest
                {
                    Id = row.Field<int>("Id"),
                    status = (TestStatusEnum)row.Field<int>("Status"),
                    Message = row.Field<string>("Message") ?? "",
                    attachment = row.Field<Attachment>("Attachment"), // Ajustar según el tipo real de Attachment
                    idUser = row.Field<int>("UserId")
                });
            }

            return changeStatusTestsList;
        }

        public async Task<ChangeStatusTest> GetByIdAsync(int id)
        {
            SqlParameter[] parameters = {
                new SqlParameter("@ChangeStatusTestId", id)
            };

            DataTable result = await _dbConnect.GetDataSPAsync("GetChangeStatusTestById", parameters);

            if (result.Rows.Count == 0)
                throw new Exception("ChangeStatusTest not found");

            DataRow row = result.Rows[0];
            return new ChangeStatusTest
            {
                Id = row.Field<int>("Id"),
                status = (TestStatusEnum)row.Field<int>("Status"),
                Message = row.Field<string>("Message") ?? "",
                attachment = row.Field<Attachment>("Attachment"), // Ajustar según el tipo real de Attachment
                idUser = row.Field<int>("UserId")
            };
        }

        public async Task<DBResponse> RemoveAsync(ChangeStatusTest entity)
        {
            SqlParameter[] parameters = {
                new SqlParameter("@ChangeStatusTestId", entity.Id)
            };

            DataTable result = await _dbConnect.GetDataSPAsync("DeleteChangeStatusTest", parameters);

            return new DBResponse
            {
                id = result.Rows[0].Field<int>("Id"),
                message = result.Rows[0].Field<string>("Message") ?? ""
            };
        }

        public async Task<DBResponse> UpdateAsync(ChangeStatusTest entity)
        {
            SqlParameter[] parameters = {
                new SqlParameter("@ChangeStatusTestId", entity.Id),
                new SqlParameter("@Status", (int)entity.status), // Asumiendo que TestStatusEnum se puede convertir a int
                new SqlParameter("@Message", entity.Message),
                new SqlParameter("@AttachmentId", entity.attachment != null ? (object)entity.attachment.Id : DBNull.Value), // Ajusta según la estructura de tu Attachment
                new SqlParameter("@UserId", entity.idUser)
            };

            DataTable result = await _dbConnect.GetDataSPAsync("UpdateChangeStatusTest", parameters);

            return new DBResponse
            {
                id = result.Rows[0].Field<int>("Id"),
                message = result.Rows[0].Field<string>("Message") ?? ""
            };
        }

        public async Task<DBResponse> AssignChangeStatusToTest(ChangeStatusTest changeStatus)
        {
            SqlParameter[] parameters = {
                new SqlParameter("@TestId", changeStatus.idUser), // Ajustar según tu lógica
                new SqlParameter("@ChangeStatusTestID", (int)changeStatus.status),
                new SqlParameter("@Message", changeStatus.Message)
            };

            DataTable result = await _dbConnect.GetDataSPAsync("AssignChangeStatusToTest", parameters);

            return new DBResponse
            {
                id = result.Rows[0].Field<int>("Id"),
                message = result.Rows[0].Field<string>("Message") ?? ""
            };
        }

        public async Task<DBResponse> RemoveChangeStatusFromTest(ChangeStatusTest changeStatus)
        {
            SqlParameter[] parameters = {
                new SqlParameter("@TestId", changeStatus.idUser), // Ajustar según tu lógica
                new SqlParameter("@ChangeStatusTestID", (int)changeStatus.status)
            };

            DataTable result = await _dbConnect.GetDataSPAsync("RemoveChangeStatusFromTest", parameters);

            return new DBResponse
            {
                id = result.Rows[0].Field<int>("Id"),
                message = result.Rows[0].Field<string>("Message") ?? ""
            };
        }

        public Task<IEnumerable<ChangeStatusTest>> FindAsync(Expression<Func<ChangeStatusTest, bool>> predicate)
        {
            throw new NotImplementedException();
        }
    }
}
