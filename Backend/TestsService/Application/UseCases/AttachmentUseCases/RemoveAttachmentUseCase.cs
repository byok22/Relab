using AutoMapper;
using Domain.Repositories;
using Domain.Services;
using Shared.Dtos;
using Shared.Response;

namespace Application.UseCases.AttachmentUseCases
{
    public class RemoveAttachmentUseCase
    {
         private readonly IMapper _mapper;
        private readonly IAttachmentRepository _repository;
        private readonly IFDataService _fDataService;

        public RemoveAttachmentUseCase(IMapper mapper, IAttachmentRepository repository, IFDataService fDataService)
        {
            _mapper = mapper;
            _repository = repository;
            _fDataService = fDataService;
        }

        public async Task<GenericResponse> Execute(AttachmentDto attachmentDto)
        {
            if (attachmentDto == null)
                throw new ArgumentNullException(nameof(attachmentDto));

            // Mapeo del DTO a la entidad de dominio Attachment
            var attachment = _mapper.Map<Domain.Models.Attachment>(attachmentDto);

            // Eliminar el archivo existente
            var removeResponse = await _fDataService.RemoveAttachment(attachment);
            if (!removeResponse.IsSuccessful)
            {
                return new GenericResponse
                {
                    IsSuccessful = false,
                    Message = $"Not removed  {attachment.Name}"
                };
            }

             // Actualizar el attachment en la base de datos
            var updateResponse = await _repository.RemoveAsync(attachment);

            return new GenericResponse
            {
                IsSuccessful = updateResponse.id>0?true:false,
                Message = updateResponse.message
            };
            
        }
        
    }
}