using Domain.Models;
using Domain.Services;
using Shared.Dtos;
using Shared.Response;

namespace Infrastructure.Services
{
    public class FDataService : IFDataService
    {
        private readonly string _fDataPhysical=@"\\Mxgdld0nsifsn03\gdl_te_vol_1\TE\ApplicationData\WebApps\ReilabTest";
        private readonly string _fDataVirtual=@"http://mxgdlm0webme01/FData/ReilabTest";
     
        public FDataService()
        {
            //_fDataPhysical = fDataPhysical ?? throw new ArgumentNullException(nameof(fDataPhysical));
            //_fDataVirtual = fDataVirtual ?? throw new ArgumentNullException(nameof(fDataVirtual));
           
        }

        public async Task<Attachment> Save(Attachment attachment)
        {
            if (attachment == null)
                throw new ArgumentNullException(nameof(attachment));

            // Asegurarse de que el directorio de _fDataPhysical existe
            if (!Directory.Exists(_fDataPhysical))
            {
                Directory.CreateDirectory(_fDataPhysical);
            }

            // Crear el path completo del archivo (ruta física)
            var filePath = Path.Combine(_fDataPhysical, attachment.Name);

            // Guardar el archivo en la ubicación física
            await File.WriteAllBytesAsync(filePath, attachment.File);

            // Crear la URL virtual a partir del nombre del archivo
            string virtualUrl = Path.Combine(_fDataVirtual, attachment.Name);

            //cambiar los diagonal invertidos por diagonal normales para la url
            virtualUrl = virtualUrl.Replace(@"\", "/");

            // Actualizar el DTO con los valores calculados
            attachment.Location = filePath;   // Ruta física completa donde se guardó el archivo
            attachment.Url = virtualUrl;      // URL completa para descargar el archivo

       

            // Aquí podrías guardar `attachment` en una base de datos, si es necesario.

            return attachment;
        }

        public async Task<Attachment> GetAttachment(Attachment attachment)
        {
            if (attachment == null)
                throw new ArgumentNullException(nameof(attachment));

            // Verificar si el archivo existe en la ubicación física
            var filePath = Path.Combine(_fDataPhysical, attachment.Name);
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"Archivo {attachment.Name} no encontrado en {filePath}");
            }

            // Leer el archivo y mapearlo a Attachmen
            var fileBytes = await File.ReadAllBytesAsync(filePath);
            var attachmen = new Attachment
            {
                Name = attachment.Name,
                File = fileBytes,
                Location = filePath,
                Url = Path.Combine(_fDataVirtual, attachment.Name).Replace(@"\", "/")
            };

            return attachmen;
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
