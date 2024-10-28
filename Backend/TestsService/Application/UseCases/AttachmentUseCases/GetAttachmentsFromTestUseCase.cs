using AutoMapper;
using Domain.Repositories;
using Shared.Dtos;

namespace Application.UseCases.AttachmentUseCases
{
    public class GetAttachmentsFromTestUseCase
    {
         private readonly IMapper _mapper;
        private readonly IAttachmentRepository _repository;
        public GetAttachmentsFromTestUseCase(IMapper mapper, IAttachmentRepository repository)
        {
            _mapper = mapper;
            _repository = repository;           
        }

        public async Task<List<AttachmentDto>> Execute(int idTest)
        {
            if (idTest <= 0)
                throw new ArgumentException("Invalid attachment ID", nameof(idTest));

            // Obtener el attachment desde el repositorio
            var attachments = await _repository.GetAttachmentsByTestID(idTest);

            if (attachments == null)
                return null; // O lanzar una excepción si prefieres

            // Mapear la entidad de dominio a DTO
            var attachmentsDto = _mapper.Map<List<AttachmentDto>>(attachments);

            // Opcional: Aquí podrías utilizar _fDataService si necesitas realizar más operaciones

            return attachmentsDto;
        }
    }
}