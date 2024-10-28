using AutoMapper;
using Domain.Repositories;
using Domain.Services;
using Shared.Dtos;


namespace Application.UseCases.AttachmentUseCases
{
    public class GetAttachmentByIDUseCase
    {
        private readonly IMapper _mapper;
        private readonly IAttachmentRepository _repository;
        private readonly IFDataService _fDataService;

        public GetAttachmentByIDUseCase(IMapper mapper, IAttachmentRepository repository, IFDataService fDataService)
        {
            _mapper = mapper;
            _repository = repository;
            _fDataService = fDataService;
        }

        public async Task<AttachmentDto> Execute(int idAttachment)
        {
            if (idAttachment <= 0)
                throw new ArgumentException("Invalid attachment ID", nameof(idAttachment));

            // Obtener el attachment desde el repositorio
            var attachment = await _repository.GetByIdAsync(idAttachment);

            if (attachment == null)
                return null; // O lanzar una excepción si prefieres

            // Mapear la entidad de dominio a DTO
            var attachmentDto = _mapper.Map<AttachmentDto>(attachment);

            // Opcional: Aquí podrías utilizar _fDataService si necesitas realizar más operaciones

            return attachmentDto;
        }
    }
}
