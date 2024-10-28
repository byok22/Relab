

namespace Domain.Models
{
    public class Attachment
    {
        public int Id { get; set; }
        public string Name { get; set; }=string.Empty;
        public string Location { get; set; }=string.Empty;
        public string Url { get; set; }= string.Empty;
        public byte[]? File { get; set; }= new byte[0];
    }
}