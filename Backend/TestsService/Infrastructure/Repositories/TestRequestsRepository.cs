
using System.Data;
using Domain.Models;
using Shared.Response;

using Microsoft.Data.SqlClient;
using Domain.DataBase;
using Domain.Repositories;
using Domain.Models.TestRequests;
using Domain.Enums;
using Infrastructure.Models;
using Domain.Models.Employees;
using Shared.Dtos;
using System.Linq.Expressions;

namespace Infrastructure.Repositories
{
    public class TestRequestsRepository : ITestRequestRepository
    {
        private readonly ISQLDbConnect _dbConnect;

        public TestRequestsRepository(ISQLDbConnect dbConnect)
        {
            _dbConnect = dbConnect;
        }

        public async Task<TestRequest> AddAsync(TestRequest entity)
        {
            SqlParameter[] parameters = {
                new SqlParameter("@Status", entity.Status.ToString()),
                new SqlParameter("@Description", entity.Description),
                new SqlParameter("@Start", entity.Start),
                new SqlParameter("@End", entity.End),
                new SqlParameter("@Active", entity.Active),
                new SqlParameter("@CreatedBy", entity.CreatedBy?.Id??0)                
            };

            DataTable result = await _dbConnect.GetDataSPAsync("CreateTestRequest", parameters);

            return new TestRequest
            {
                Id = result.Rows[0].Field<int>("Id"),
                Status = Enum.TryParse<TestRequestsStatus>(result.Rows[0].Field<string>("Status"), true, out var status) ? status : TestRequestsStatus.New,
                Description = result.Rows[0].Field<string>("Description"),
                Start = result.Rows[0].Field<DateTime>("Start"),
                End = result.Rows[0].Field<DateTime>("End"),
                Active = result.Rows[0].Field<bool>("Active"),
                CreatedAt = result.Rows[0].Field<DateTime>("CreatedAt"),
                CreatedBy = new User { Id = int.Parse(result.Rows[0].Field<string>("CreatedBy")) },
            };
        }

        public async Task<DBResponse> RemoveAsync(TestRequest entity)
        {
            SqlParameter[] parameters = {
                new SqlParameter("@Id", entity.Id)
            };

            DataTable result = await _dbConnect.GetDataSPAsync("DeleteTestRequest", parameters);

            return new DBResponse
            {
                id = result.Rows[0].Field<int>("Id"),
                message = result.Rows[0].Field<string>("Message") ?? ""
            };
        }

        public async Task<IEnumerable<TestRequest>> GetAllAsync()
        {
            DataTable result = await _dbConnect.GetDataSPAsync("GetAllTestRequests", null);
            List<TestRequest> testRequestsList = new List<TestRequest>();

            foreach (DataRow row in result.Rows)
            {
                testRequestsList.Add(new TestRequest
                {
                    Id = row.Field<int>("Id"),
                    Status =  Enum.TryParse<TestRequestsStatus>(result.Rows[0].Field<string>("Status"), true, out var status) ? status : TestRequestsStatus.New,
                    Description = row.Field<string>("Description"),
                    Start = row.Field<DateTime>("Start"),
                    End = row.Field<DateTime>("End"),
                    Active = row.Field<bool>("Active"),
                    CreatedAt = row.Field<DateTime>("CreatedAt"),                  
                    CreatedBy = new User { Id = int.Parse(result.Rows[0].Field<string>("CreatedBy")) },
                });
            }

            return testRequestsList;
        }

        public async Task<TestRequest> GetByIdAsync(int id)
        {
            SqlParameter[] parameters = {
                new SqlParameter("@Id", id)
            };

            DataTable result = await _dbConnect.GetDataSPAsync("GetTestRequestById", parameters);

            if (result.Rows.Count == 0)
                throw new Exception("Test request not found");

            DataRow row = result.Rows[0];
            return new TestRequest
            {
                Id = row.Field<int>("Id"),
                Status =  Enum.TryParse<TestRequestsStatus>(result.Rows[0].Field<string>("Status"), true, out var status) ? status : TestRequestsStatus.New,
                Description = row.Field<string>("Description"),
                Start = row.Field<DateTime>("Start"),
                End = row.Field<DateTime>("End"),
                Active = row.Field<bool>("Active"),
                CreatedAt = row.Field<DateTime>("CreatedAt"),
                CreatedBy = new User { Id = int.Parse(result.Rows[0].Field<string>("CreatedBy")) },
            };
        }

        public async Task<DBResponse> UpdateAsync(TestRequest entity)
        {
            SqlParameter[] parameters = {
                new SqlParameter("@Id", entity.Id),
                new SqlParameter("@Status", entity.Status),
                new SqlParameter("@Description", entity.Description),
                new SqlParameter("@Start", entity.Start),
                new SqlParameter("@End", entity.End),
                new SqlParameter("@Active", entity.Active),
              
            };

            DataTable result = await _dbConnect.GetDataSPAsync("UpdateTestRequest", parameters);

            return new DBResponse
            {
                id = result.Rows[0].Field<int>("Id"),
                message = result.Rows[0].Field<string>("Message") ?? ""
            };
        }

        public async Task<IEnumerable<Test>> GetTestsByRequestAsync(int testRequestId)
        {
            SqlParameter[] parameters = {
                new SqlParameter("@TestRequestID", testRequestId)
            };

            DataTable result = await _dbConnect.GetDataSPAsync("GetTestsByRequest", parameters);
            List<Test> testsList = new List<Test>();
             List<Ar_Test> testList = new List<Ar_Test>();
            foreach (DataRow row in result.Rows){
                testList.Add(new Ar_Test
                {
                    Id = row.Field<int>("Id"),
                    Name = row.Field<string>("Name"),
                    Description = row.Field<string>("Description"),
                    Start = row.Field<DateTime?>("Start"),
                    End = row.Field<DateTime?>("End"),
                    SamplesId = row.Field<int?>("SamplesId"),
                    SamplesQuantity = row.Field<int?>("SamplesQuantity"),
                    SamplesWeight = row.Field<decimal?>("SamplesWeight"),
                    SamplesSize = row.Field<decimal?>("SamplesSize"),
                    SpecialInstructions = row.Field<string>("SpecialInstructions"),
                    ProfileId = row.Field<int?>("ProfileId"),
                    ProfileName = row.Field<string>("ProfileName"),
                    ProfileUrl = row.Field<string>("ProfileUrl"),
                    EnginnerId = row.Field<int?>("EnginnerId"),
                    Status = row.Field<string>("Status"),
                    LastUpdatedMessage = row.Field<string>("LastUpdatedMessage"),
                    IdRequest = row.Field<int?>("IdRequest"),
                    CreatedAt = row.Field<DateTime>("CreatedAt"),
                    UpdatedAt = row.Field<DateTime>("UpdatedAt"),
                    UpdatedBy = row.Field<string>("UpdatedBy"),
                    CreatedBy = row.Field<string>("CreatedBy")
                });
                //complete the rest of the fields testsList
               
            }
           foreach(var test in testList)
                {
                    testsList.Add(new Test
                    {
                        Id = test.Id,
                        Name = test.Name,
                        Description = test.Description,
                        Start = test.Start ?? DateTime.MinValue,
                        End = test.End ?? DateTime.MinValue,
                        Samples = new Samples
                        {
                            Id = test.SamplesId ?? 0,
                            Quantity = test.SamplesQuantity ?? 0,
                            Weight = test.SamplesWeight ?? 0,
                            Size = test.SamplesSize ?? 0
                        },
                        SpecialInstructions = test.SpecialInstructions,
                        Profile = new Attachment
                        {
                            Id = test.ProfileId ?? 0,
                            Name = test.ProfileName,
                            Location = test.ProfileUrl
                        },
                        Enginner = new Employee
                        {
                            Id = test.EnginnerId ?? 0
                        },
                        Status = Enum.TryParse<TestStatusEnum>(test.Status, true, out var status) ? status : TestStatusEnum.New,
                        LastUpdatedMessage = test.LastUpdatedMessage,
                        idRequest = test.IdRequest,
                        CreatedAt = test.CreatedAt,
                        UpdatedAt = test.UpdatedAt,
                        CreatedBy = test.CreatedBy,
                        UpdatedBy = test.UpdatedBy
                    });
                }

           

            return testsList;
        }

        public async Task<DBResponse> AssignTestToRequestAsync(int testRequestId, int testId)
        {
            SqlParameter[] parameters = {
                new SqlParameter("@TestRequestID", testRequestId),
                new SqlParameter("@TestId", testId)
            };

            DataTable result = await _dbConnect.GetDataSPAsync("AssignTestToRequest", parameters);

            return new DBResponse
            {
                id = result.Rows[0].Field<int>("Id"),
                message = result.Rows[0].Field<string>("Message") ?? ""
            };
        }

        public async Task<DBResponse> RemoveTestFromRequestAsync(int testRequestId, int testId)
        {
            SqlParameter[] parameters = {
                new SqlParameter("@TestRequestID", testRequestId),
                new SqlParameter("@TestId", testId)
            };

            DataTable result = await _dbConnect.GetDataSPAsync("RemoveTestFromRequest", parameters);

            return new DBResponse
            {
                id = result.Rows[0].Field<int>("Id"),
                message = result.Rows[0].Field<string>("Message") ?? ""
            };
        }

        public async Task<List<TestRequest>> GetTestRequestsByStatus(TestRequestsStatus? status)
        {
            SqlParameter[] parameters = {
                new SqlParameter("@Status", status.ToString().ToUpper())
            };
            DataTable dataTable = await _dbConnect.GetDataSPAsync("GetTestRequestsByStatus", parameters);
        
            List<TestRequest> testRequests = new List<TestRequest>();
            foreach (DataRow row in dataTable.Rows)
            {
                testRequests.Add(new TestRequest
                {
                    Id = row.Field<int>("Id"),
                    Status = Enum.TryParse<TestRequestsStatus>(row.Field<string>("Status"), true, out var status2) ?  (TestRequestsStatus)(status2) : TestRequestsStatus.New,
                    Description = row.Field<string>("Description"),
                    Start = row.Field<DateTime>("Start"),
                    End = row.Field<DateTime>("End"),
                    Active = row.Field<bool>("Active"),
                    CreatedAt = row.Field<DateTime>("CreatedAt"),
                    CreatedBy = new User { Id = int.Parse(row.Field<string>("CreatedBy")) },
                    TestsCount = row.Field<int>("TestsCount")
                });
            }
        
            return testRequests; // Add this return statement
        }

        public async Task<TestRequest> AproveOrDennyTestRequest(int id, ChangeStatusTestRequest changeStatusTestRequest)
        {
            SqlParameter[] parameters = {
                new SqlParameter("@Id", id),
                new SqlParameter("@Status", changeStatusTestRequest.status),
                new SqlParameter("@Reason", changeStatusTestRequest.Message)
            };

            DataTable result = await _dbConnect.GetDataSPAsync("AproveOrDennyTestRequest", parameters);

            if (result.Rows.Count == 0)
                throw new Exception("Test request not found");

            DataRow row = result.Rows[0];
            return new TestRequest
            {
                Id = row.Field<int>("Id"),
                Status =  Enum.TryParse<TestRequestsStatus>(result.Rows[0].Field<string>("Status"), true, out var status) ? status : TestRequestsStatus.New,
                Description = row.Field<string>("Description"),
                Start = row.Field<DateTime>("Start"),
                End = row.Field<DateTime>("End"),
                Active = row.Field<bool>("Active"),
                CreatedAt = row.Field<DateTime>("CreatedAt"),
                CreatedBy = new User { Id = int.Parse(result.Rows[0].Field<string>("CreatedBy")) },
            };
        }
        public async Task<TestRequest> ChangeStatusTestRequest(int id, ChangeStatusTestRequest changeStatusTestRequest)
        {
            SqlParameter[] parameters = {
                new SqlParameter("@Id", id),
                new SqlParameter("@Status", changeStatusTestRequest.status),
                new SqlParameter("@Reason", changeStatusTestRequest.Message)
            };

            DataTable result = await _dbConnect.GetDataSPAsync("ChangeStatusTestRequest", parameters);

            if (result.Rows.Count == 0)
                throw new Exception("Test request not found");

            DataRow row = result.Rows[0];
            return new TestRequest
            {
                Id = row.Field<int>("Id"),
                Status =  Enum.TryParse<TestRequestsStatus>(result.Rows[0].Field<string>("Status"), true, out var status) ? status : TestRequestsStatus.New,
                Description = row.Field<string>("Description"),
                Start = row.Field<DateTime>("Start"),
                End = row.Field<DateTime>("End"),
                Active = row.Field<bool>("Active"),
                CreatedAt = row.Field<DateTime>("CreatedAt"),
                CreatedBy = new User { Id = int.Parse(result.Rows[0].Field<string>("CreatedBy")) },
            };
        }

        public Task<IEnumerable<TestRequest>> FindAsync(Expression<Func<TestRequest, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Test>> GetTestOfTestRequest(int idTestRequest)
        {
            /*
            CREATE PROCEDURE GetTestsOfTestRequest 
	-- Add the parameters for the stored procedure here
	@TestRequestId int = 0
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT  * from dbo.AR_Tests a
	where a.IdRequest = @TestRequestId


END
GO

            */
            SqlParameter[] parameters = {
                new SqlParameter("@TestRequestId", idTestRequest)
            };
           
            DataTable result = await _dbConnect.GetDataSPAsync("GetTestsOfTestRequest", parameters);
            List<Ar_Test> testList = new List<Ar_Test>();
                List<Test> tests = new List<Test>();


                foreach (DataRow row in result.Rows)
                {
                    testList.Add(new Ar_Test
                    {
                        Id = row.Field<int>("Id"),
                        Name = row.Field<string>("Name"),
                        Description = row.Field<string>("Description"),
                        Start = row.Field<DateTime?>("Start"),
                        End = row.Field<DateTime?>("End"),
                        SamplesId = row.Field<int?>("SamplesId"),
                        SpecialInstructions = row.Field<string>("SpecialInstructions"),
                        ProfileId = row.Field<int?>("ProfileId"),
                        EnginnerId = row.Field<int?>("EnginnerId"),
                        Status = row.Field<string>("Status"),
                        LastUpdatedMessage = row.Field<string>("LastUpdatedMessage"),
                        IdRequest = row.Field<int?>("IdRequest"),
                        CreatedAt = row.Field<DateTime>("CreatedAt"),
                        UpdatedAt = row.Field<DateTime>("UpdatedAt"),
                        CreatedBy = row.Field<string>("CreatedBy"),
                        UpdatedBy = row.Field<string>("UpdatedBy"),
                        ProfileName = row.Field<string>("ProfileName"),
                        ProfileUrl = row.Field<string>("ProfileUrl"),
                        SamplesQuantity = row.Field<int?>("SamplesQuantity"),
                        SamplesWeight = row.Field<decimal?>("SamplesWeight"),
                        SamplesSize = row.Field<decimal?>("SamplesSize")
                    });
                }
                foreach(var test in testList)
                {
                    tests.Add(new Test
                    {
                        Id = test.Id,
                        Name = test.Name,
                        Description = test.Description,
                        Start = test.Start ?? DateTime.MinValue,
                        End = test.End ?? DateTime.MinValue,
                        Samples = new Samples
                        {
                            Id = test.SamplesId ?? 0,
                            Quantity = test.SamplesQuantity ?? 0,
                            Weight = test.SamplesWeight ?? 0,
                            Size = test.SamplesSize ?? 0
                        },
                        SpecialInstructions = test.SpecialInstructions,
                        Profile = new Attachment
                        {
                            Id = test.ProfileId ?? 0,
                            Name = test.ProfileName??"",
                            Location = test.ProfileUrl??""
                        },
                        Enginner = new Employee
                        {
                            Id = test.EnginnerId ?? 0
                        },
                        Status = Enum.TryParse<TestStatusEnum>(test.Status, true, out var status) ? status : TestStatusEnum.New,
                        LastUpdatedMessage = test.LastUpdatedMessage,
                        idRequest = test.IdRequest,
                        CreatedAt = test.CreatedAt,
                        UpdatedAt = test.UpdatedAt,
                        CreatedBy = test.CreatedBy,
                        UpdatedBy = test.UpdatedBy
                    });
                }
                return tests;
        }
    }
}
