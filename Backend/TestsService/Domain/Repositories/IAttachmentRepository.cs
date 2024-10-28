using Domain.Models;

namespace Domain.Repositories
{
    public interface IAttachmentRepository: IGenericRepository<Attachment>
    {
        public Task<List<Attachment>> GetAttachmentsByTestID(int testID);
          
    }
}