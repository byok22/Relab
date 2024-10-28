
namespace Shared.Dtos
{
    public class CustomerDto
    {
        public int Id { get; set; }     
        //UuId
        public string CustomerID { get; set; } = string.Empty;
        public string CustomerName { get; set; } = string.Empty;
        public string Division { get; set; } = string.Empty;
        public int BuildingID { get; set; } 
        public string Building { get; set; } = string.Empty;
        public bool Available { get; set; } = true;
        
    }
}