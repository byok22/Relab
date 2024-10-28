

using Domain.Enums;

namespace Domain.Models.Tests
{
    public class ChangeStatusTest
    {
                public TestStatusEnum status { get; set; }
                public string Message { get; set; }=string.Empty;
                public Attachment? attachment{ get; set; }
                public int idUser { get; set; }  
                public int idTest { get; set; }         
    }
}
