
using Domain.Models;
using Shared.Dtos;
using Shared.Response;

namespace Domain.Services
{
    public interface IFDataService
    {
        Task<Attachment> Save(Attachment attachmentDto);
        Task<Attachment> GetAttachment(Attachment attachment);
        Task<GenericResponse> RemoveAttachment(Attachment attachment);
        
    }
}