
using Domain.Enums;
using Domain.Models.Generics;

namespace Domain.Models.TestRequests
{
    public class TestRequest
    {
         public int Id { get; set; }
        public TestRequestsStatus Status {get; set; }
        public string Description { get; set; }= string.Empty;                     
        public DateTime Start {get; set; }
        public DateTime End {get; set; }
        //public Attachment? Profile { get; set; }
        public List<Test>? Tests{get; set;}
        public int? TestsCount {get; set;}
        public bool Active {get; set; }  
        public User? CreatedBy {get; set; }   
        public DateTime CreatedAt {get; set; }
        public List<GenericUpdate>? Updates {get; set;}
        public List<ChangeStatusTestRequest>? Changes{get; set;}
        
    }
}