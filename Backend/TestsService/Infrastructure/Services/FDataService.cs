using AutoMapper;
using Domain.Models;
using Domain.Services;
using Shared.Dtos;
using Shared.Response;

namespace Infrastructure.Services
{
    public class FDataService : IFDataService
    {
        private readonly string _fDataPhysical;
        private readonly string _fDataVirtual;
        private readonly IMapper _mapper;

        public FDataService(string fDataPhysical, string fDataVirtual, IMapper mapper)
        {
            _fDataPhysical = fDataPhysical ?? throw new ArgumentNullException(nameof(fDataPhysical));
            _fDataVirtual = fDataVirtual ?? throw new ArgumentNullException(nameof(fDataVirtual));
            _mapper = mapper;
        }

        public async Task<Attachment> Save(AttachmentDto attachmentDto)
        {
            if (attachmentDto == null)
                throw new ArgumentNullException(nameof(attachmentDto));

            // Asegurarse de que el directorio de _fDataPhysical existe
            if (!Directory.Exists(_fDataPhysical))
            {
                Directory.CreateDirectory(_fDataPhysical);
            }

            // Crear el path completo del archivo (ruta física)
            var filePath = Path.Combine(_fDataPhysical, attachmentDto.Name);

            // Guardar el archivo en la ubicación física
            await File.WriteAllBytesAsync(filePath, attachmentDto.File);

            // Crear la URL virtual a partir del nombre del archivo
            string virtualUrl = Path.Combine(_fDataVirtual, attachmentDto.Name);

            // Actualizar el DTO con los valores calculados
            attachmentDto.Location = filePath;   // Ruta física completa donde se guardó el archivo
            attachmentDto.Url = virtualUrl;      // URL completa para descargar el archivo

            // Mapeo del DTO a la entidad de dominio Attachment
            var attachment = _mapper.Map<Attachment>(attachmentDto);

            // Aquí podrías guardar `attachment` en una base de datos, si es necesario.

            return attachment;
        }

        public async Task<AttachmentDto> GetAttachment(Attachment attachment)
        {
            if (attachment == null)
                throw new ArgumentNullException(nameof(attachment));

            // Verificar si el archivo existe en la ubicación física
            var filePath = Path.Combine(_fDataPhysical, attachment.Name);
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"Archivo {attachment.Name} no encontrado en {filePath}");
            }

            // Leer el archivo y mapearlo a AttachmentDto
            var fileBytes = await File.ReadAllBytesAsync(filePath);
            var attachmentDto = new AttachmentDto
            {
                Name = attachment.Name,
                File = fileBytes,
                Location = filePath,
                Url = Path.Combine(_fDataVirtual, attachment.Name)
            };

            return attachmentDto;
        }

        public async Task<GenericResponse> RemoveAttachment(Attachment attachment)
        {
            if (attachment == null)
                throw new ArgumentNullException(nameof(attachment));

            // Obtener la ruta física del archivo
            var filePath = Path.Combine(_fDataPhysical, attachment.Name);

            // Verificar si el archivo existe
            if (!File.Exists(filePath))
            {
                return new GenericResponse
                {
                    IsSuccessful = false,
                    Message = $"Archivo {attachment.Name} no encontrado."
                };
            }

            // Intentar eliminar el archivo
            try
            {
                File.Delete(filePath);
                return new GenericResponse
                {
                    IsSuccessful = true,
                    Message = $"Archivo {attachment.Name} eliminado exitosamente."
                };
            }
            catch (Exception ex)
            {
                return new GenericResponse
                {
                    IsSuccessful = false,
                    Message = $"Error al eliminar el archivo {attachment.Name}: {ex.Message}"
                };
            }
        }
    }
}
