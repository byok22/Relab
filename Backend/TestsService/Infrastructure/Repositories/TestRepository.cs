using System.Data;
using Microsoft.Data.SqlClient;
using System.Linq.Expressions;
using Domain.DataBase;
using Domain.Enums;
using Domain.Models;
using Domain.Models.Employees;
using Domain.Models.Equipments;
using Domain.Repositories;
using Infrastructure.Models;
using Microsoft.Data.SqlClient;
using Shared.Response;
using Domain.Models.Generics;

namespace Infrastructure.Repositories
{
    public class TestRepository : ITestRepository
    {
        private readonly ISQLDbConnect _dbConnect;

        public TestRepository(ISQLDbConnect sQLDbConnect)
        {
            _dbConnect = sQLDbConnect;
        }

        
    public async Task<Test> AddAsync(Test entity)
    {
        try
        {
            SqlParameter[] parameters = {
                new SqlParameter("@Name", entity.Name),
                new SqlParameter("@Description", entity.Description),
                new SqlParameter("@Start", entity.Start ),
                new SqlParameter("@End", entity.End ),
                new SqlParameter("@SamplesId", entity.Samples?.Id ?? 0),
                new SqlParameter("@SpecialInstructions", entity.SpecialInstructions ?? (object)DBNull.Value),
                new SqlParameter("@ProfileId", entity.Profile?.Id ?? 0),
                new SqlParameter("@EnginnerId", entity.Enginner?.Id ?? 0),
                new SqlParameter("@Status", entity.Status.ToString() ),
                new SqlParameter("@LastUpdatedMessage", entity.LastUpdatedMessage ?? (object)DBNull.Value),
                new SqlParameter("@IdRequest", entity.idRequest),
                new SqlParameter("@CreatedBy", entity.CreatedBy)
            };
            _dbConnect.CloseConnection();

            DataTable result =  _dbConnect.GetDataSP("CreateTest", parameters);

            var Ar_Test = new Ar_Test
            {
                Id = result.Rows[0].Field<int>("Id"),
                Name = result.Rows[0].Field<string>("Name"),
                Description = result.Rows[0].Field<string>("Description"),
                Start = result.Rows[0].Field<DateTime?>("Start"),
                End = result.Rows[0].Field<DateTime?>("End"),
                SamplesId = result.Rows[0].Field<int?>("SamplesId"),
                SpecialInstructions = result.Rows[0].Field<string>("SpecialInstructions"),
                ProfileId = result.Rows[0].Field<int?>("ProfileId"),
                EnginnerId = result.Rows[0].Field<int?>("EnginnerId"),
                Status = result.Rows[0].Field<string>("Status"),
                LastUpdatedMessage = result.Rows[0].Field<string>("LastUpdatedMessage"),
                IdRequest = result.Rows[0].Field<int?>("IdRequest"),
                CreatedAt = result.Rows[0].Field<DateTime>("CreatedAt"),
                UpdatedAt = result.Rows[0].Field<DateTime>("UpdatedAt"),
                CreatedBy = result.Rows[0].Field<string>("CreatedBy"),
                UpdatedBy = result.Rows[0].Field<string>("UpdatedBy")
            };
            entity.Id = Ar_Test.Id;
            return entity;
        }
        catch (Exception ex)
        {
            throw new Exception("Error adding test", ex);
        }
    }
        public Task<IEnumerable<Test>> FindAsync(Expression<Func<Test, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Test>> GetAllAsync()
        {
            try
            {
                DataTable result = await _dbConnect.GetDataSPAsync("GetAllTests", null);
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
                        UpdatedBy = row.Field<string>("UpdatedBy")
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
                return tests;

            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving all tests", ex);
            }
        }

        public async Task<IEnumerable<Test>> GetAllByStatusAsync(TestStatusEnum testStatusEnum)
        {
            try
            {
                SqlParameter[] parameters = {
                    new SqlParameter("@Status", testStatusEnum.ToString())
                };
                DataTable result = _dbConnect.GetDataSPAsync("GetTestsByStatus", parameters).Result;
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
                        SamplesSize = row.Field<decimal?>("SamplesSize"),
                        AttachmentsCount = row.Field<int>("AttachmentsCount"),
                        TechniciansCount = row.Field<int>("TechniciansCount"),
                        SpecificationsCount = row.Field<int>("SpecificationsCount"),
                        EquipmentsCount = row.Field<int>("EquipmentsCount"),
                        EnginnerName = row.Field<string>("EnginnerName")?? string.Empty,


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
                            Name = test.ProfileName,
                            Location = test.ProfileUrl
                        },
                        Enginner = new Employee
                        {
                            Id = test.EnginnerId ?? 0,
                            Name = test.EnginnerName??string.Empty
                        },
                        Status = Enum.TryParse<TestStatusEnum>(test.Status, true, out var status) ? status : TestStatusEnum.New,
                        LastUpdatedMessage = test.LastUpdatedMessage,
                        idRequest = test.IdRequest,
                        CreatedAt = test.CreatedAt,
                        UpdatedAt = test.UpdatedAt,
                        CreatedBy = test.CreatedBy,
                        UpdatedBy = test.UpdatedBy,
                        AttachmentsCount = test.AttachmentsCount,
                        TechniciansCount = test.TechniciansCount,
                        SpecificationsCount = test.SpecificationsCount,
                        EquipmentsCount = test.EquipmentsCount
                    });
                }
                return tests;
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving tests by status", ex);
            }
        }

        public Task<Test> GetByIdAsync(int id)
        {
            try
            {/*
            Create PROCEDURE GetTestById
    @TestId INT =1
AS*/
                Ar_Test testAr = new Ar_Test();
                SqlParameter[] parameters = {
                    new SqlParameter("@TestId", id)
                };
                DataTable result = _dbConnect.GetDataSPAsync("GetTestById", parameters).Result;
                if (result.Rows.Count > 0)
                {
                    testAr = new Ar_Test
                    {
                        Id = result.Rows[0].Field<int>("Id"),
                        Name = result.Rows[0].Field<string>("Name"),
                        Description = result.Rows[0].Field<string>("Description"),
                        Start = result.Rows[0].Field<DateTime?>("Start"),
                        End = result.Rows[0].Field<DateTime?>("End"),
                        SamplesId = result.Rows[0].Field<int?>("SamplesId"),
                        SpecialInstructions = result.Rows[0].Field<string>("SpecialInstructions"),
                        ProfileId = result.Rows[0].Field<int?>("ProfileId"),
                        EnginnerId = result.Rows[0].Field<int?>("EnginnerId"),
                        EmployeeNumber = result.Rows[0].Field<string>("EmployeeNumber"),
                        EnginnerName = result.Rows[0].Field<string>("EnginnerName"),
                        Status = result.Rows[0].Field<string>("Status"),
                        LastUpdatedMessage = result.Rows[0].Field<string>("LastUpdatedMessage"),
                        IdRequest = result.Rows[0].Field<int?>("IdRequest"),
                        CreatedAt = result.Rows[0].Field<DateTime>("CreatedAt"),
                        UpdatedAt = result.Rows[0].Field<DateTime>("UpdatedAt"),
                        CreatedBy = result.Rows[0].Field<string>("CreatedBy"),
                        UpdatedBy = result.Rows[0].Field<string>("UpdatedBy"),
                        ProfileName = result.Rows[0].Field<string>("ProfileName"),
                        ProfileUrl = result.Rows[0].Field<string>("ProfileUrl"),
                        SamplesQuantity = result.Rows[0].Field<int?>("SamplesQuantity"),
                        SamplesWeight = result.Rows[0].Field<decimal?>("SamplesWeight"),
                        SamplesSize = result.Rows[0].Field<decimal?>("SamplesSize")                        
                    };
                }
                Test test = new Test
                {
                    Id = testAr.Id,
                    Name = testAr.Name,
                    Description = testAr.Description,
                    Start = testAr.Start ?? DateTime.MinValue,
                    End = testAr.End ?? DateTime.MinValue,
                    Samples = new Samples
                    {
                        Id = testAr.SamplesId ?? 0,
                        Quantity = testAr.SamplesQuantity ?? 0,
                        Weight = testAr.SamplesWeight ?? 0,
                        Size = testAr.SamplesSize ?? 0
                    },
                    SpecialInstructions = testAr.SpecialInstructions,
                    Profile = new Attachment
                    {
                        Id = testAr.ProfileId ?? 0,
                        Name = testAr.ProfileName??"",
                        Url = testAr.ProfileUrl??"",
                       
                    },
                    Enginner = new Employee
                    {
                        Id = testAr.EnginnerId ?? 0,
                        EmployeeNumber = testAr.EmployeeNumber??string.Empty,
                        Name = testAr.EnginnerName??string.Empty
                    },
                    Status = Enum.TryParse<TestStatusEnum>(testAr.Status, true, out var status) ? status : TestStatusEnum.New,
                    LastUpdatedMessage = testAr.LastUpdatedMessage,
                    idRequest = testAr.IdRequest,
                    CreatedAt = testAr.CreatedAt,
                    UpdatedAt = testAr.UpdatedAt,
                    CreatedBy = testAr.CreatedBy,
                    UpdatedBy = testAr.UpdatedBy
                };
                
              
              
                return Task.FromResult(test);
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving test by id", ex);
            }
        }

        public async Task<DBResponse> UpdateAsync(Test entity)
        {
            try
            {
                var responseDb = new DBResponse
                {
                    id = entity.Id
                };
                if (await GetByIdAsync(entity.Id) == null)
                {
                    responseDb.message = "Not Found";
                    return responseDb;
                }

                SqlParameter[] parameters = {
                    new SqlParameter("@TestId", entity.Id),
                    new SqlParameter("@Name", entity.Name),
                    new SqlParameter("@Description", entity.Description),
                    new SqlParameter("@Start", entity.Start),
                    new SqlParameter("@End", entity.End),
                    new SqlParameter("@SamplesId", entity.Samples.Id),
                    new SqlParameter("@SpecialInstructions", entity.SpecialInstructions),
                    new SqlParameter("@ProfileId", entity.Profile.Id),
                    new SqlParameter("@EnginnerId", entity.Enginner.Id),
                    new SqlParameter("@Status", entity.Status.ToString()),
                    new SqlParameter("@LastUpdatedMessage", entity.LastUpdatedMessage??string.Empty),
                    new SqlParameter("@UpdatedBy", entity.UpdatedBy)
                };

                DataTable result = await _dbConnect.GetDataSPAsync("UpdateTest", parameters);

                if (result.Rows.Count > 0)
                {
                    responseDb.message = result.Rows[0].Field<string>("message");
                }
                else
                {
                    responseDb.message = "Update Failed";
                }

                return responseDb;
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating test", ex);
            }
        }

        public Task<DBResponse> RemoveAsync(Test entity)
        {
            throw new NotImplementedException();
        }

        public async Task<DBResponse> RemoveProfileFromTest(int idTest)
        {
             SqlParameter[] parameters = {
                new SqlParameter("@TestId", idTest)
            };
            DataTable result = await _dbConnect.GetDataSPAsync("RemoveProfileFromTest", parameters);
            
            var response = new DBResponse
            {
                id = result.Rows[0].Field<int>("id"),
                message = result.Rows[0].Field<string>("message") ?? string.Empty
            };
            return response;
        }

        public async Task<DBResponse> UpdateDatesOfTest(int idTest, DateTime startDate, DateTime endDate)
        {
           SqlParameter[] parameters = {

                new SqlParameter("@TestId", idTest),
                new SqlParameter("@StartDate", startDate),
                new SqlParameter("@EndDate", endDate)
            };
            DataTable result = await _dbConnect.GetDataSPAsync("UpdateDatesOfTest", parameters);
            var response = new DBResponse
            {
                id = result.Rows[0].Field<int>("id"),
                message = result.Rows[0].Field<string>("message") ?? string.Empty
            };
            return response;

        }
    }
}