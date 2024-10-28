
namespace Shared.Response
{
    public class GenericResponse
    {
        public bool IsSuccessful { get; set; }
        public string Message { get; set; }= string.Empty;
        public int? Id { get; set; }
    }
}