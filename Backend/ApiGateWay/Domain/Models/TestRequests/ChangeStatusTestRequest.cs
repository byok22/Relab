
using Domain.Enums;

namespace Domain.Models.TestRequests
{
    public class ChangeStatusTestRequest
    {
        public TestRequestsStatus Status { get; set; }
        public string Message { get; set; }=string.Empty;
        public Attachment? Attachment{ get; set; }
        public int IdUser { get; set; }            
    }
}