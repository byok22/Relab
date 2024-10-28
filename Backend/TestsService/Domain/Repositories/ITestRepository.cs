using Domain.Enums;
using Domain.Models;
using Shared.Response;


namespace Domain.Repositories
{
    public interface ITestRepository: IGenericRepository<Test>
    {
        Task<IEnumerable<Test>> GetAllByStatusAsync(TestStatusEnum testStatusEnum);
        Task<DBResponse> RemoveProfileFromTest(int idTest);

        Task<DBResponse> UpdateDatesOfTest(int idTest, DateTime startDate, DateTime endDate);
    }
}