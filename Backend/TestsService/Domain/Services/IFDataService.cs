
using Domain.Models;
using Shared.Dtos;
using Shared.Response;

namespace Domain.Services
{
    public interface IFDataService
    {
        Task<Attachment> Save(AttachmentDto attachmentDto);
        Task<AttachmentDto> GetAttachment(Attachment attachment);
        Task<GenericResponse> RemoveAttachment(Attachment attachment);
        
    }
}