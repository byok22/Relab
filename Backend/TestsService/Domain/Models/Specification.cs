namespace Domain.Models
{
    public class Specification
    {
        public int Id { get; set; }
        public string? SpecificationName { get; set; }
        public string Details { get; set; }="";
        
    }
}