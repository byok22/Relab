
using Domain.Enums;


namespace Domain.Models
{
    public class ChangeStatusTest
    {
        public int Id { get; set; }
                public TestStatusEnum status { get; set; }
                public string Message { get; set; }=string.Empty;
                public Attachment? attachment{ get; set; }
                public int idUser { get; set; } 
                public int idTest { get; set; } 
                          
    }
}

