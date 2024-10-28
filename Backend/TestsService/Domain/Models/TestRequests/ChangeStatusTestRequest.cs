
using Domain.Enums;

namespace Domain.Models.TestRequests
{
    public class ChangeStatusTestRequest
    {
        public TestRequestsStatus status { get; set; }
        public string Message { get; set; }=string.Empty;
        public Attachment? attachment{ get; set; }
        public int idUser { get; set; }            
    }
}