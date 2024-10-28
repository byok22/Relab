namespace Shared.Dtos
{
    public class AttachmentDto{ 
        public int Id { get; set; }   
        public string Name { get; set; }=string.Empty;
        public Byte[] File { get; set; }= new byte[0];
        public string Url { get; set; }= string.Empty;
        public string Location { get; set; }= string.Empty ;
    }
}