using Domain.Models.TestRequests;

namespace Shared.Request
{
    public class ChangeStatusTestRequestApi
    {
        public int id { get; set; }
        public ChangeStatusTestRequest? changeStatusTestRequest { get; set; }
    }
}